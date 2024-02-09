namespace InternshipsAppApi.DTO
{
    public record InternshipCreateDTO
    {
        public string AdminUserID { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Domain { get; set; }
        public string Description { get; set; }
        public string Deadline { get; set; }
    }
}
