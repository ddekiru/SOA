using Domain.Domain;
using Infrastructure.Persistence.Entities;

namespace Infrastructure.Persistence
{
    public static class EntityUtils
    {
        public static AdminUser DbAdminUserToAdminUser(DbAdminUser dbAdminUser)
            => new()
            {
                Id = dbAdminUser.Id,
                Username = dbAdminUser.Username,
                CompanyName = dbAdminUser.CompanyName
            };

        public static DbAdminUser AdminUserToDbAdminUser(AdminUser adminUser)
            => new()
            {
                Id = adminUser.Id,
                Username = adminUser.Username,
                CompanyName = adminUser.CompanyName
            };

        public static RegularUser DbRegularUserToRegularUser(DbRegularUser dbRegularUser)
            => new()
            {
                Id = dbRegularUser.Id,
                Username = dbRegularUser.Username
            };

        public static DbRegularUser RegularUserToDbRegularUser(RegularUser regularUser)
            => new()
            {
                Id = regularUser.Id,
                Username = regularUser.Username
            };

        public static Internship DbInternshipToInternship(DbInternship dbInternship)
            => new()
            {
                Id = dbInternship.Id,
                AdminUser = DbAdminUserToAdminUser(dbInternship.AdminUser),
                NoApplications = dbInternship.NoApplications,
                Title = dbInternship.Title,
                Location = dbInternship.Location,
                Domain = dbInternship.Domain,
                Description = dbInternship.Description,
                DateAdded = dbInternship.DateAdded,
                Deadline = dbInternship.Deadline
            };

        public static DbInternship InternshipToDbInternship(Internship internship)
            => new()
            {
                Id = internship.Id,
                AdminUserId = internship.AdminUser.Id,
                NoApplications = internship.NoApplications,
                Title = internship.Title,
                Location = internship.Location,
                Domain = internship.Domain,
                Description = internship.Description,
                DateAdded = internship.DateAdded,
                Deadline = internship.Deadline
            };

        public static InternshipApplication DbInternshipApplicationToInternshipApplication(DbInternshipApplication dbInternshipApplication)
            => new()
            {
                Internship = DbInternshipToInternship(dbInternshipApplication.Internship),
                RegularUser = DbRegularUserToRegularUser(dbInternshipApplication.RegularUser)
            };

        public static DbInternshipApplication InternshipApplicationToDbInternshipApplication(InternshipApplication internshipApplication)
            => new()
            {
                InternshipId = internshipApplication.Internship.Id,
                RegularUserId = internshipApplication.RegularUser.Id
            };
    }
}
