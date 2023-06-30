using HospitalManagement.Models.DTO;

namespace HospitalManagement.Interface
{
    public interface IGenerateToken
    {
        public string? GenerateToken(UserDTO userDTO);
    }
}
