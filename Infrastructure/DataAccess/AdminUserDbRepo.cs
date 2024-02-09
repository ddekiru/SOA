using Domain.Domain;
using Domain.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public class AdminUserDbRepo : IAdminUserRepo
    {
        private readonly InternshipsContext _dbContext;

        public AdminUserDbRepo(InternshipsContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public async Task<AdminUser> Add(AdminUser user, CancellationToken cancellationToken = default)
        {
            var admUser = await _dbContext.AdminUsers
                .SingleOrDefaultAsync(a => a.Username.Equals(user.Username), cancellationToken);
            if (admUser != null)
                throw new RepositoryException("There already is an admin user with this username");
            var adminUser = EntityUtils.AdminUserToDbAdminUser(user);
            await _dbContext.AdminUsers.AddAsync(adminUser, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            user.Id = adminUser.Id;
            return user;
        }

        public async Task<AdminUser> GetById(string id, CancellationToken cancellationToken = default)
        {
            var dbAdminUser = await _dbContext.AdminUsers
                .SingleOrDefaultAsync(a => a.Id.Equals(id), cancellationToken);
            if (dbAdminUser == null)
                return null;
            var adminUser = EntityUtils.DbAdminUserToAdminUser(dbAdminUser);
            return adminUser;
        }

        public async Task<AdminUser> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            var dbAdminUser = await _dbContext.AdminUsers
                .SingleOrDefaultAsync(a => a.Username.Equals(username), cancellationToken);
            if (dbAdminUser == null)
                return null;
            var adminUser = EntityUtils.DbAdminUserToAdminUser(dbAdminUser);
            return adminUser;
        }
    }
}
