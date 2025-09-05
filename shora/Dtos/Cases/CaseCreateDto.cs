namespace shora.Dtos.Cases
{
    public class CaseCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string FileUrl { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Governorate { get; set; }

        // ممكن يحدد الكلاينت اللي أنشأ القضية
        public int? UserId { get; set; }
    }
}
