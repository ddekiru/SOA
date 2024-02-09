namespace InternshipsAppApi.DTO
{
    public record InternshipApplicationCreateDTO
    {
        public int InternshipId { get; set; }
        public string RegularUserId { get; set; }
    }
}
