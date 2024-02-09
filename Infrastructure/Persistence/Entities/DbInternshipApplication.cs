namespace Infrastructure.Persistence.Entities
{
    public class DbInternshipApplication
    {
        public int InternshipId { get; set; }
        public DbInternship Internship { get; set; } = null!;
        public string RegularUserId { get; set; }
        public DbRegularUser RegularUser { get; set; } = null!;
    }
}
