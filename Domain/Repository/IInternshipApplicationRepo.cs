using Domain.Domain;

namespace Domain.Repository
{
    public interface IInternshipApplicationRepo
    {
        Task<InternshipApplication> Add(InternshipApplication internshipApplication, CancellationToken cancellationToken = default);
        Task Delete(int internshipId, string regularUserId, CancellationToken cancellationToken = default);
        Task<IEnumerable<InternshipApplication>> GetByRegularUserId(string regularUserId, CancellationToken cancellationToken = default);
        Task<IEnumerable<InternshipApplication>> GetByInternshipId(int internshipId, CancellationToken cancellationToken = default);
        Task<InternshipApplication> GetById(string userId, int internshipId, CancellationToken cancellationToken = default);
    }
}
