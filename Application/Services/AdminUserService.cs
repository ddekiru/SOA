using Application.Services.Interfaces;
using Domain.Domain;
using Domain.Repository;

namespace Application.Services
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IAdminUserRepo _adminUserRepo;

        public AdminUserService(IAdminUserRepo adminUserRepo)
        {
            _adminUserRepo = adminUserRepo;
        }

        public async Task CheckFields(string username, string companyName, CancellationToken cancellationToken = default)
            => await AdminUser.Validate(username, companyName, cancellationToken);

        public async Task<AdminUser> CreateAdminUser(string id, string username, string companyName,
            CancellationToken cancellationToken = default)
        {
            AdminUser adminUser = new()
            {
                Id = id,
                Username = username,
                CompanyName = companyName
            };
            return await _adminUserRepo.Add(adminUser, cancellationToken);
        }

        public Task<AdminUser> FindAdminUserById(string adminUserId,
            CancellationToken cancellationToken = default)
            => _adminUserRepo.GetById(adminUserId, cancellationToken);

        public async Task<AdminUser> FindAdminUserByUsername(string username,
            CancellationToken cancellationToken = default)
            => await _adminUserRepo.GetByUsernameAsync(username, cancellationToken);
    }
}
