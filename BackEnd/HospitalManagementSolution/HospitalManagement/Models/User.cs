using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordKey { get; set; }
    }
}
