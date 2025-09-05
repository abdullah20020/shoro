namespace shora.Dtos.Cases
{
    public class CaseUpdateDto
    {
        public int Id { get; set; }             // لازم نعرف أي قضية هنعدل
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? FileUrl { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Governorate { get; set; }
        public string? Status
        {
            get; set;
        }
    }
}
