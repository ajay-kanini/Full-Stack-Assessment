using HospitalManagement.Models;
using HospitalManagement.Models.DTO;

namespace HospitalManagement.Interface
{
    public interface IManageUsers
    {
        public Task<UserDTO> DoctorRegistration(DoctorDTO doctorDTO);
        public Task<UserDTO> PatientRegistration(PatientDTO patientDTO);
        public Task<UserDTO> Login(UserDTO userDTO);
        public Task<User> UpdateDoctor(User user);
        public Task<UserDTO> GetUserByMail(UserDTO userDTO);
    }
}
