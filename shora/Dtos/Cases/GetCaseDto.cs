namespace shora.Dtos.Cases
{
    public class GetCaseDto
    {
        public int Id { get; set; }           // جاي من BaseClass
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string FileUrl { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Governorate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }

        // ممكن تعرض بيانات اليوزر
        public int? UserId { get; set; }
        public string? UserName { get; set; }   // لو عايز تجيب اسم الكلاينت
    }
}
