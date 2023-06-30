using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? Specialization { get; set; }
        public string? Qualifications { get; set; }     
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
