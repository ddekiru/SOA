namespace Infrastructure.Persistence.Entities
{
    public class DbInternship
    {
        public int Id { get; set; }
        public string AdminUserId { get; set; }
        public DbAdminUser AdminUser { get; set; } = null!;
        public string Title { get; set; }
        public string Location { get; set; }
        public string Domain { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime Deadline { get; set; }
        public int NoApplications { get; set; }
        public ICollection<DbInternshipApplication> Applications { get; set; }
    }
}
