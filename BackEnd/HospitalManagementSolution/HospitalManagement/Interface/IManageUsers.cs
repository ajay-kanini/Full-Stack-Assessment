﻿using HospitalManagement.Models;
using HospitalManagement.Models.DTO;

namespace HospitalManagement.Interface
{
    public interface IManageUsers
    {
        public Task<UserDTO> DoctorRegistration(DoctorDTO doctorDTO);
        public Task<UserDTO> PatientRegistration(PatientDTO patientDTO);
        public Task<UserDTO> Login(UserDTO userDTO);
        public Task<Doctor> UpdateDoctor(Doctor doctor);
        public Task<UserDTO> GetUserByMail(UserDTO userDTO);
        public Task<ICollection<Doctor>> GetAllDoctors();
        public Task<Doctor> GetDoctor(int key);
    }
}
