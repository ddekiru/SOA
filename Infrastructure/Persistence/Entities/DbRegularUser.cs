namespace Infrastructure.Persistence.Entities
{
    public class DbRegularUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public ICollection<DbInternshipApplication> Applications { get; set; }
    }
}
