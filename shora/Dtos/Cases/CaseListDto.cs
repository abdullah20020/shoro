namespace shora.Dtos.Cases
{
    public class CaseListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
