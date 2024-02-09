using Application.Services.Interfaces;
using Domain.Domain;
using Domain.Repository;

namespace Application.Services
{
    public class InternshipService : IInternshipService
    {
        private readonly IInternshipRepo _internshipRepo;

        public InternshipService(IInternshipRepo internshipRepo)
        {
            _internshipRepo = internshipRepo;
        }

        public async Task<Internship> CreateInternship(string adminUserId, string title, string location, string domain,
            string description, DateTime dateAdded, DateTime deadline, CancellationToken cancellationToken = default)
        {
            Internship internship = new()
            {
                AdminUser = new AdminUser { Id = adminUserId },
                Title = title,
                Location = location,
                Domain = domain,
                Description = description,
                DateAdded = dateAdded,
                Deadline = deadline
            };
            return await _internshipRepo.Add(internship, cancellationToken);
        }

        public Task DeleteInternship(int tripId, CancellationToken cancellationToken = default)
            => _internshipRepo.Delete(tripId, cancellationToken);

        public Task<Internship> FindInternshipById(int id, CancellationToken cancellationToken = default)
            => _internshipRepo.GetById(id, cancellationToken);

        public Task<IEnumerable<Internship>> FindInternshipsFiltered(string title, string location, string domain,
            string companyName, DateTime? date = null, CancellationToken cancellationToken = default)
            => _internshipRepo.GetFiltered(title, location, domain, companyName, date, cancellationToken);
    }
}
