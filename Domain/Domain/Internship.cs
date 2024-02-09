namespace Domain.Domain
{
    public class Internship
    {
        public int Id { get; set; }
        public AdminUser AdminUser { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Domain { get; set; }
        public string Description { get; set; }
        public int NoApplications { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime Deadline { get; set; }
    }
}
