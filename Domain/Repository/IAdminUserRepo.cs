using Domain.Domain;

namespace Domain.Repository
{
    public interface IAdminUserRepo
    {
        Task<AdminUser> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<AdminUser> Add(AdminUser user, CancellationToken cancellationToken = default);
        Task<AdminUser> GetById(string id, CancellationToken cancellationToken = default);
    }
}
