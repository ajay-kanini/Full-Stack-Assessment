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
            doctorDTO.Users.Role = "Doctor";
            doctorDTO.Status = "Not Approved";
            var userResult = await _userRepo.Add(doctorDTO.Users);
            if(userResult == null) return null;
            doctorDTO.Id = userResult.Id;
            var doctorResult = await _doctorRepo.Add(doctorDTO);
            if (userResult != null && doctorResult != null)
            {
                user = new UserDTO();
                user.Id = userResult.Id;
                user.Role = userResult.Role;
                user.Token = _generateToken.GenerateToken(user);
            }
            return user;
        }

        public async Task<UserDTO> PatientRegistration(PatientDTO patientDTO)
        {
            UserDTO? user = null;
            var hmac = new HMACSHA512();
            patientDTO.Users.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(patientDTO.Password ?? "1234"));
            patientDTO.Users.PasswordKey = hmac.Key;
            patientDTO.Users.Role = "Patient";
            var userResult = await _userRepo.Add(patientDTO.Users);
            if (userResult == null) return null;
            patientDTO.Id = userResult.Id;
            var patientResult = await _patientRepo.Add(patientDTO);
            if (userResult != null && patientResult != null)
            {
                user = new UserDTO();
                user.Id = patientResult.Id;
                user.Role = userResult.Role;
                user.Token = _generateToken.GenerateToken(user);
            }
            return user;
        }
        public async Task<UserDTO> Login(UserDTO userDTO)
        {
            userDTO=await GetUserByMail(userDTO);
            var userData = await _userRepo.Get(userDTO.Id);
            if (userData != null)
            {
                var hmac = new HMACSHA512(userData.PasswordKey);
                var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for (int i = 0; i < userPass.Length; i++)
                {
                    if (userPass[i] != userData.PasswordHash[i])
                        return null;
                }
                userDTO = new UserDTO();
                userDTO.Mail = userData.Mail; 
                userDTO.Id = userData.Id;
                userDTO.Role = userData.Role;
                userDTO.Token = _generateToken.GenerateToken(userDTO);
            }
            return userDTO;
        }

        public async Task<Doctor> UpdateDoctor(Doctor doctor)
        {
            var checkUser = await _doctorRepo.Update(doctor);
            if (checkUser != null)
                return checkUser;
            else
                return null;
        }

        public async Task<UserDTO> GetUserByMail(UserDTO userDTO)
        {
            var users = await _userRepo.GetAll();
            var user = users.FirstOrDefault(u => u.Mail == userDTO.Mail);
            userDTO.Id = user.Id;
            return userDTO;
        }

        public async Task<ICollection<Doctor>> GetAllDoctors()
        {
            var doctors = await _doctorRepo.GetAll();
            if (doctors != null)
            {
                return doctors;
            }
            else
                return null;
        }
    }
}
