using Domain.Domain;

namespace Application.Services.Interfaces
{
    public interface IInternshipService
    {
        Task<Internship> CreateInternship(string adminUserId, string title, string location, string domain, string description,
            DateTime dateAdded, DateTime deadline, CancellationToken cancellationToken = default);
        Task<IEnumerable<Internship>> FindInternshipsFiltered(string title, string location, string domain,
            string companyName, DateTime? date = null, CancellationToken cancellationToken = default);
        Task<Internship> FindInternshipById(int id, CancellationToken cancellationToken = default);
        Task DeleteInternship(int tripId, CancellationToken cancellationToken = default);
    }
}
