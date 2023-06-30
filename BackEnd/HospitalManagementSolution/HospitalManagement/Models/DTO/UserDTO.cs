namespace HospitalManagement.Models.DTO
{
    public class UserDTO
    {
        public string? Mail { get; set; }
        public int Id { get; set; }   
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
    }
}
