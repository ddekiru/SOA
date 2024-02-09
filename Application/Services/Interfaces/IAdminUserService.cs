using Domain.Domain;

namespace Application.Services.Interfaces
{
    public interface IAdminUserService
    {
        Task CheckFields(string username, string companyName, CancellationToken cancellationToken = default);
        Task<AdminUser> CreateAdminUser(string id, string username, string companyName,
            CancellationToken cancellationToken = default);
        Task<AdminUser> FindAdminUserByUsername(string username,
            CancellationToken cancellationToken = default);
        Task<AdminUser> FindAdminUserById(string adminUserId, CancellationToken cancellationToken = default);
    }
}
