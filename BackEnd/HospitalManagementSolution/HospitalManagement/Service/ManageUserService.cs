using HospitalManagement.Interface;
using HospitalManagement.Models;
using HospitalManagement.Models.DTO;
using System.Security.Cryptography;
using System.Text;

namespace HospitalManagement.Service
{
    public class ManageUserService : IManageUsers
    {
        private IRepo<Doctor, int> _doctorRepo;
        private IRepo<Patient, int> _patientRepo;
        private IRepo<User, int> _userRepo;
        private IGenerateToken _generateToken;

        public ManageUserService(
            IRepo<Doctor, int> doctorRepo,
            IRepo<Patient, int> patientRepo,
            IRepo<User, int> userRepo,
            IGenerateToken generateToken)
        {
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;
            _userRepo = userRepo;
            _generateToken = generateToken;
        }
        public async Task<UserDTO> DoctorRegistration(DoctorDTO doctorDTO)
        {
            UserDTO? user = null;
            var hmac = new HMACSHA512();
            doctorDTO.Users.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(doctorDTO.Password ?? "1234"));
            doctorDTO.Users.PasswordKey = hmac.Key;
            doctorDTO.Users.Role = "Admin";
            doctorDTO.Users.Status = "Approved";
            var userResult = await _userRepo.Add(doctorDTO.Users);
            if(userResult == null) return null;
            doctorDTO.Id = userResult.UserId;
            var doctorResult = await _doctorRepo.Add(doctorDTO);
            if (userResult != null && doctorResult != null)
            {
                user = new UserDTO();
                user.Id = doctorResult.Id;
                user.Role = userResult.Role;
                user.Token = _generateToken.GenerateToken(user);
            }
            return user;
        }

        public Task<UserDTO> PatientRegistration(PatientDTO patientDTO)
        {
            throw new NotImplementedException();
        }
        public Task<UserDTO> Login(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> UpdateDoctor(User user)
        {
            throw new NotImplementedException();
        }
    }
}
