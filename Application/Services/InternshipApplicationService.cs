using Application.Services.Interfaces;
using Domain.Domain;
using Domain.Repository;

namespace Application.Services
{
    public class InternshipApplicationService : IInternshipApplicationService
    {
        private readonly IInternshipApplicationRepo _internshipApplicationRepo;

        public InternshipApplicationService(IInternshipApplicationRepo internshipApplicationRepo)
        {
            _internshipApplicationRepo = internshipApplicationRepo;
        }

        public Task<InternshipApplication> CreateInternshipApplication(int internshipId, string regularUserId, CancellationToken cancellationToken = default)
        {
            InternshipApplication internshipApplication = new()
            {
                Internship = new Internship { Id = internshipId },
                RegularUser = new RegularUser { Id = regularUserId }
            };
            return _internshipApplicationRepo.Add(internshipApplication, cancellationToken);
        }

        public Task DeleteInternshipApplication(int internshipId, string regularUserId, CancellationToken cancellationToken = default)
            => _internshipApplicationRepo.Delete(internshipId, regularUserId, cancellationToken);

        public Task<InternshipApplication> FindInternshipApplicationById(string userId, int internshipId, CancellationToken cancellationToken = default)
            => _internshipApplicationRepo.GetById(userId, internshipId, cancellationToken);

        public Task<IEnumerable<InternshipApplication>> FindInternshipApplicationsByInternshipId(int internshipId, CancellationToken cancellationToken = default)
            => _internshipApplicationRepo.GetByInternshipId(internshipId, cancellationToken);

        public Task<IEnumerable<InternshipApplication>> FindInternshipApplicationssByRegularUserId(string regularUserId, CancellationToken cancellationToken = default)
            => _internshipApplicationRepo.GetByRegularUserId(regularUserId, cancellationToken);
    }
}
