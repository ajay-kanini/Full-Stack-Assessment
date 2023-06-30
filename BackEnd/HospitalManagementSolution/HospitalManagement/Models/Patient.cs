using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
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
        public string? Address { get; set; }
    }
}
