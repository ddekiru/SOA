using Domain.Domain;

namespace Domain.Repository
{
    public interface IInternshipRepo
    {
        Task<Internship> Add(Internship internship, CancellationToken cancellationToken = default);
        Task Delete(int internshipId, CancellationToken cancellationToken = default);
        Task<Internship> GetById(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Internship>> GetFiltered(string title, string location, string domain,
            string companyName = "", DateTime? date = null, 
            CancellationToken cancellationToken = default);
    }
}
