using Domain.Domain;
using Domain.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public class InternshipDbRepo : IInternshipRepo
    {
        private readonly InternshipsContext _dbContext;

        public InternshipDbRepo(InternshipsContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public async Task<Internship> Add(Internship internship, CancellationToken cancellationToken = default)
        {
            var dbInternship = EntityUtils.InternshipToDbInternship(internship);
            var adminUser = EntityUtils.DbAdminUserToAdminUser(await _dbContext.AdminUsers
                .SingleOrDefaultAsync(a => a.Id.Equals(dbInternship.AdminUserId), cancellationToken));
            if (adminUser == null)
                throw new RepositoryException("The agency doesn't exist");
            internship.AdminUser = null;
            await _dbContext.Internships.AddAsync(dbInternship, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            internship.AdminUser = adminUser;
            internship.Id = dbInternship.Id;
            return internship;
        }

        public async Task Delete(int internshipId, CancellationToken cancellationToken = default)
        {
            var dbInternship = await _dbContext.Internships.
                SingleOrDefaultAsync(i => i.Id.Equals(internshipId), cancellationToken);
            if (dbInternship == null)
                throw new RepositoryException("No trip with this id");
            _dbContext.Internships.Remove(dbInternship);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Internship> GetById(int id, CancellationToken cancellationToken = default)
        {
            var dbInternship = await _dbContext.Internships.SingleOrDefaultAsync(t => t.Id.Equals(id), cancellationToken);
            if (dbInternship == null)
                return null;
            dbInternship.AdminUser = await _dbContext.AdminUsers
                .SingleOrDefaultAsync(a => a.Id.Equals(dbInternship.AdminUserId), cancellationToken);
            var internship = EntityUtils.DbInternshipToInternship(dbInternship);
            internship.NoApplications = await _dbContext.InternshipsApplications
                .Where(i => i.InternshipId == id)
                .CountAsync(cancellationToken: cancellationToken);
            return internship;
        }

        public async Task<IEnumerable<Internship>> GetFiltered(string title, string location, string domain,
            string companyName = "", DateTime? date = null, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Internships
                .Where(i =>
                    i.Deadline > DateTime.Now &&
                    i.Title.Contains(title) &&
                    i.Location.Contains(location) &&
                    i.Domain.Contains(domain) &&
                    i.AdminUser.CompanyName.Contains(companyName));

            if (date != null)
                query = query.Where(i => i.Deadline < date);

            var filteredInternshipEntities = await query
                .OrderByDescending(i => i.DateAdded)
                .ToListAsync(cancellationToken);

            foreach (var internship in filteredInternshipEntities)
            {
                internship.AdminUser = await _dbContext.AdminUsers
                    .SingleOrDefaultAsync(a => a.Id.Equals(internship.AdminUserId), cancellationToken);
                internship.NoApplications = await _dbContext.InternshipsApplications
                    .Where(a => a.InternshipId == internship.Id)
                    .CountAsync(cancellationToken: cancellationToken);
            }
            var internships = filteredInternshipEntities
                .Select(t => EntityUtils.DbInternshipToInternship(t));
            return internships;
        }
    }
}
