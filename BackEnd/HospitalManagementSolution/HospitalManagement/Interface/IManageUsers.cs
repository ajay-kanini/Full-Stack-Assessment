using HospitalManagement.Models.DTO;

namespace HospitalManagement.Interface
{
    public interface IManageUsers
    {
        public Task<Doctors> DoctorRegistration(Doctors doctorDTO);
        public Task<PatientDTO> PatientRegistration(PatientDTO patientDTO);
        public Task<UserDTO> Login(UserDTO userDTO);
    }
}
