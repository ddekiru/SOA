using Domain.Domain;

namespace Application.Services.Interfaces
{
    public interface IInternshipApplicationService
    {
        Task<InternshipApplication> CreateInternshipApplication(int internshipId, string regularUserId, CancellationToken cancellationToken = default);
        Task<IEnumerable<InternshipApplication>> FindInternshipApplicationssByRegularUserId(string regularUserId,
            CancellationToken cancellationToken = default);
        Task<IEnumerable<InternshipApplication>> FindInternshipApplicationsByInternshipId(int internshipId,
            CancellationToken cancellationToken = default);
        Task<InternshipApplication> FindInternshipApplicationById(string userId, int internshipId, CancellationToken cancellationToken = default);
        Task DeleteInternshipApplication(int internshipId, string regularUserId, CancellationToken cancellationToken = default);
    }
}
