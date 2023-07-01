using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Models
{
    public class Doctor
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age
        {
            get
            {
                var year = DateTime.Now.Year - DateOfBirth.Year;
                if (DateTime.Now.Month > DateOfBirth.Month)
                    year--;
                return year;
            }
            set
            {
                Age = value;
            }
        }
        public string? Gender { get; set; }
        public string? Specialization { get; set; }
        public string? Qualifications { get; set; }     
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public User? Users { get; set; }
    }
}
