using Domain.Domain;

namespace Application.Services.Interfaces
{
    public interface IRegularUserService
    {
        Task<RegularUser> CreateRegularUser(string regularUserId, string username,
            CancellationToken cancellationToken = default);
        Task<RegularUser> FindRegularUserByUsername(string username,
            CancellationToken cancellationToken = default);
        Task<RegularUser> FindRegularUserById(string regularUserId, CancellationToken cancellationToken = default);
        Task CheckFields(string userName, CancellationToken cancellationToken = default);
    }
}
