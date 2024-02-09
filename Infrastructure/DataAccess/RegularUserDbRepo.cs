using Domain.Domain;
using Domain.Repository;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public class RegularUserDbRepo : IRegularUserRepo
    {
        private readonly InternshipsContext _dbContext;

        public RegularUserDbRepo(InternshipsContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public async Task<RegularUser> Add(RegularUser regularUser, CancellationToken cancellationToken = default)
        {
            var r = await _dbContext.RegularUsers
                .SingleOrDefaultAsync(r => r.Username.Equals(regularUser.Username), cancellationToken);
            if (r != null)
                throw new RepositoryException("There already exists a regular user with this username");
            var dbRegularUser = EntityUtils.RegularUserToDbRegularUser(regularUser);
            await _dbContext.RegularUsers.AddAsync(dbRegularUser, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            regularUser.Id = regularUser.Id;
            return regularUser;
        }

        public async Task<RegularUser> GetById(string regularUserId, CancellationToken cancellationToken = default)
        {
            var dbRegularUser = await _dbContext.RegularUsers
                .SingleOrDefaultAsync(r => r.Id.Equals(regularUserId), cancellationToken);
            if (dbRegularUser == null)
                return null;
            var regularUser = EntityUtils.DbRegularUserToRegularUser(dbRegularUser);
            return regularUser;
        }

        public async Task<RegularUser> GetByUsername(string username, CancellationToken cancellationToken = default)
        {
            var regularUser = await _dbContext.RegularUsers
                .SingleOrDefaultAsync(r => r.Username.Equals(username), cancellationToken);
            if (regularUser == null)
                return null;
            var dRegularUser = EntityUtils.DbRegularUserToRegularUser(regularUser);
            return dRegularUser;
        }
    }
}
