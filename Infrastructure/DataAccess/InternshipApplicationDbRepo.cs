using Domain.Domain;
using Domain.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public class InternshipApplicationDbRepo : IInternshipApplicationRepo
    {
        private readonly InternshipsContext _dbContext;

        public InternshipApplicationDbRepo(InternshipsContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public async Task<InternshipApplication> Add(InternshipApplication internshipApplication, CancellationToken cancellationToken = default)
        {
            var dbInternship = await _dbContext.Internships
                .SingleOrDefaultAsync(t => t.Id.Equals(internshipApplication.Internship.Id), cancellationToken);
            if (dbInternship == null)
                throw new RepositoryException("The internship doesn't exist");

            dbInternship.AdminUser = await _dbContext.AdminUsers
                .SingleOrDefaultAsync(a => a.Id.Equals(dbInternship.AdminUserId), cancellationToken);
            if (dbInternship.AdminUser == null)
                throw new RepositoryException("The company doesn't exist");

            var regularUser = await _dbContext.RegularUsers
                .SingleOrDefaultAsync(r => r.Id.Equals(internshipApplication.RegularUser.Id), cancellationToken);
            if (regularUser == null)
                throw new RepositoryException("The user doesn't exist");

            if (await _dbContext.InternshipsApplications
                .AnyAsync(ia => ia.InternshipId == internshipApplication.Internship.Id && ia.RegularUserId == internshipApplication.RegularUser.Id, cancellationToken))
                throw new RepositoryException("You already made an application for this internship");

            var dbInternshipApplication = EntityUtils.InternshipApplicationToDbInternshipApplication(internshipApplication);
            dbInternshipApplication.Internship = null;
            dbInternshipApplication.RegularUser = null;

            await _dbContext.InternshipsApplications.AddAsync(dbInternshipApplication, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            internshipApplication.Internship = EntityUtils.DbInternshipToInternship(dbInternship);
            internshipApplication.RegularUser = EntityUtils.DbRegularUserToRegularUser(regularUser);
            return internshipApplication;
        }

        public async Task Delete(int internshipId, string regularUserId, CancellationToken cancellationToken = default)
        {
            var dbInternshipApplication = await _dbContext.InternshipsApplications
                .SingleOrDefaultAsync(r => r.InternshipId.Equals(internshipId) && r.RegularUserId.Equals(regularUserId), cancellationToken);
            if (dbInternshipApplication == null)
                throw new RepositoryException("The internship application doesn't exist");
            _dbContext.InternshipsApplications.Remove(dbInternshipApplication);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<InternshipApplication> GetById(string userId, int internshipId, CancellationToken cancellationToken = default)
        {
            var dbInternshipApplication = await _dbContext.InternshipsApplications.SingleOrDefaultAsync(
                r => r.RegularUserId == userId && r.InternshipId == internshipId, cancellationToken);
            if (dbInternshipApplication == null)
                return null;

            var dbInternship = await _dbContext.Internships
                .SingleOrDefaultAsync(t => t.Id.Equals(internshipId), cancellationToken);
            dbInternship.AdminUser = await _dbContext.AdminUsers
                .SingleOrDefaultAsync(a => a.Id.Equals(dbInternship.AdminUserId), cancellationToken);
            dbInternshipApplication.Internship = dbInternship;
            var regularUser = await _dbContext.RegularUsers
                .SingleOrDefaultAsync(r => r.Id.Equals(userId), cancellationToken);
            dbInternshipApplication.RegularUser = regularUser;

            var internshipApplication = EntityUtils.DbInternshipApplicationToInternshipApplication(dbInternshipApplication);
            return internshipApplication;
        }

        public async Task<IEnumerable<InternshipApplication>> GetByInternshipId(int internshipId, CancellationToken cancellationToken = default)
        {
            var dbInternshipApplications = await _dbContext.InternshipsApplications
                .Where(r => r.InternshipId == internshipId).ToListAsync(cancellationToken);

            foreach (var dbInternshipApplication in dbInternshipApplications)
            {
                var dbInternship = await _dbContext.Internships
                    .SingleOrDefaultAsync(t => t.Id.Equals(dbInternshipApplication.InternshipId), cancellationToken);
                dbInternship.AdminUser = await _dbContext.AdminUsers
                    .SingleOrDefaultAsync(a => a.Id.Equals(dbInternship.AdminUserId), cancellationToken);
                dbInternshipApplication.Internship = dbInternship;
                var regularUser = await _dbContext.RegularUsers
                    .SingleOrDefaultAsync(r => r.Id.Equals(dbInternshipApplication.RegularUserId), cancellationToken);
                dbInternshipApplication.RegularUser = regularUser;
            }

            var internshipApplications = dbInternshipApplications
                .Select(r => EntityUtils.DbInternshipApplicationToInternshipApplication(r));

            return internshipApplications;
        }

        public async Task<IEnumerable<InternshipApplication>> GetByRegularUserId(string regularUserId, CancellationToken cancellationToken = default)
        {
            var dbInternshipApplications = await _dbContext.InternshipsApplications
                .Where(r => r.RegularUserId == regularUserId).ToListAsync(cancellationToken: cancellationToken);

            foreach (var dbInternshipApplication in dbInternshipApplications)
            {
                var internship = await _dbContext.Internships
                    .SingleOrDefaultAsync(i => i.Id.Equals(dbInternshipApplication.InternshipId), cancellationToken);
                internship.AdminUser = await _dbContext.AdminUsers
                    .SingleOrDefaultAsync(a => a.Id.Equals(internship.AdminUserId), cancellationToken);
                dbInternshipApplication.Internship = internship;
                var regularUser = await _dbContext.RegularUsers
                    .SingleOrDefaultAsync(r => r.Id.Equals(dbInternshipApplication.RegularUserId), cancellationToken);
                dbInternshipApplication.RegularUser = regularUser;
            }

            var internshipApplications = dbInternshipApplications
                .Select(r => EntityUtils.DbInternshipApplicationToInternshipApplication(r));

            return internshipApplications;
        }
    }
}
