using HospitalManagement.Interface;
using HospitalManagement.Models;
using HospitalManagement.Models.DTO;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
            UserDTO user = new UserDTO();
            try
            {
                if (doctorDTO.Users == null)
                    throw new Exception("User in the doctorDTO is null");
                var hmac = new HMACSHA512();
                doctorDTO.Users.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(doctorDTO.Password ?? "1234"));
                doctorDTO.Users.PasswordKey = hmac.Key;
                doctorDTO.Users.Role = "Doctor";
                doctorDTO.Status = "Not Approved";

                var userResult = await _userRepo.Add(doctorDTO.Users);
                if (userResult == null)
                    throw new Exception("Failed to add user");

                doctorDTO.Id = userResult.Id;
                var doctorResult = await _doctorRepo.Add(doctorDTO);
                if (doctorResult == null)
                    throw new Exception("Failed to add doctor");

                user = new UserDTO();
                user.Id = userResult.Id;
                user.Role = userResult.Role;
                user.Token = _generateToken.GenerateToken(user);
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                Debug.WriteLine($"Doctor registration failed: {ex.Message}");
                throw new Exception("Doctor registration failed");
            }

            return user;
        }

        public async Task<UserDTO> PatientRegistration(PatientDTO patientDTO)
        {
            UserDTO user = new UserDTO();
            try
            {
                if(patientDTO.Users == null)
                    throw new Exception("User in the PatientDTO is null");
                var hmac = new HMACSHA512();
                patientDTO.Users.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(patientDTO.Password ?? "1234"));
                patientDTO.Users.PasswordKey = hmac.Key;
                patientDTO.Users.Role = "Patient";

                var userResult = await _userRepo.Add(patientDTO.Users);
                if (userResult == null)
                    throw new Exception("Failed to add user");

                patientDTO.Id = userResult.Id;
                var patientResult = await _patientRepo.Add(patientDTO);
                if (patientResult == null)
                    throw new Exception("Failed to add patient");

                user = new UserDTO();
                user.Id = patientResult.Id;
                user.Role = userResult.Role;
                user.Token = _generateToken.GenerateToken(user);
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                Debug.WriteLine($"Patient registration failed: {ex.Message}");
                throw new Exception("Patient registration failed");
            }

            return user;
        }

        public async Task<UserDTO> Login(UserDTO userDTO)
        {
            try
            {
                userDTO = await GetUserByMail(userDTO);
                if (userDTO == null)
                {
                    throw new Exception();
                }
                var userData = await _userRepo.Get(userDTO.Id);
                if (userData != null && userData.PasswordKey != null && userDTO.Password != null)
                {
                    var hmac = new HMACSHA512(userData.PasswordKey);
                    var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                    for (int i = 0; i < userPass.Length; i++)
                    {
                        if (userData == null || userData.PasswordHash == null || userData?.PasswordHash[i] == null)
                            throw new Exception("user data is null");
                        if (userPass[i] != userData?.PasswordHash[i])
                            throw new Exception("user password is wrong");
                    }

                    userDTO = new UserDTO();
                    userDTO.Mail = userData.Mail;
                    userDTO.Id = userData.Id;
                    userDTO.Role = userData.Role;
                    userDTO.Token = _generateToken.GenerateToken(userDTO);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                Debug.WriteLine($"Login failed: {ex.Message}");
                throw new Exception("Login failed");
            }

            return userDTO;
        }

        public async Task<Doctor> UpdateDoctor(Doctor doctor)
        {
            try
            {
                var checkUser = await _doctorRepo.Update(doctor);
                if (checkUser == null)
                    throw new Exception("Failed to update doctor");

                return checkUser;
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                Debug.WriteLine($"Update doctor failed: {ex.Message}");
                throw new Exception("Failed to update doctor");
            }
        }

        public async Task<UserDTO> GetUserByMail(UserDTO userDTO)
        {
            try
            {
                var users = await _userRepo.GetAll();
                if(users == null)
                {
                    throw new Exception("no data available!!!");
                }

                var user = users.FirstOrDefault(u => u.Mail == userDTO.Mail);
                if (user == null)
                    throw new Exception("User not found");

                userDTO.Id = user.Id;
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                Debug.WriteLine($"Get user by mail failed: {ex.Message}");
                throw new Exception("Get user by mail failed");
            }

            return userDTO;
        }

        public async Task<ICollection<Doctor>> GetAllDoctors()
        {
            try
            {
                var doctors = await _doctorRepo.GetAll();
                if (doctors == null)
                    throw new Exception("Failed to retrieve doctors");

                return doctors;
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                Debug.WriteLine($"Get all doctors failed: {ex.Message}");
                throw new Exception("Get all doctor failed");
            }
        }

        public async Task<Doctor> GetDoctor(int key)
        {
            try
            {
                var doctor = await _doctorRepo.Get(key);
                if (doctor == null)
                    throw new Exception("Failed to retrieve doctors");
                return doctor;
            }

            catch (Exception ex)
            {
                // Handle the exception or log the error
                Debug.WriteLine($"Get doctor failed: {ex.Message}");
                throw new Exception("Get doctor failed");
            }
        }
    }
 }
