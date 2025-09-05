using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DomianLayar.Entities
{
    public class BaseUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation Properties
        // 🔹 القضايا اللي المستخدم سجلها كـ Client
        public virtual ICollection<Case> ClientCases { get; set; } = new List<Case>();

        // 🔹 القضايا اللي المستخدم استلمها كمحامي
        public virtual ICollection<Case> LawyerCases { get; set; } = new List<Case>();
    }
}
