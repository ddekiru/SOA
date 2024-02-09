using Domain.Domain;

namespace Domain.Repository
{
    public interface IRegularUserRepo
    {
        Task<RegularUser> GetByUsername(string username, CancellationToken cancellationToken = default);
        Task<RegularUser> Add(RegularUser regularUser, CancellationToken cancellationToken = default);
        Task<RegularUser> GetById(string regularUserId, CancellationToken cancellationToken = default);
    }
}
