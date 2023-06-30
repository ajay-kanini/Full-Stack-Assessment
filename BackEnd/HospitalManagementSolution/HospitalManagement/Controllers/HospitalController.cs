using HospitalManagement.Interface;
using HospitalManagement.Models;
using HospitalManagement.Models.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class HospitalController : ControllerBase
    {
        private IManageUsers _service;

        public HospitalController(IManageUsers service)
        {
            _service = service;
        }

        [HttpPost("Doctor Registration")]
        public async Task<ActionResult<UserDTO?>> RegisterDoctor(DoctorDTO doctorDTO)
        {
            var doctor = await _service.DoctorRegistration(doctorDTO);
            if (doctor != null)
                return Created("Doctor Added", doctor);
            else
                return BadRequest("Unable to fetch");
        }

        [HttpPost("Patient Registration")]
        public async Task<ActionResult<UserDTO?>> RegisterPatient(PatientDTO patientDTO)
        {
            var patient = await _service.PatientRegistration(patientDTO);
            if (patient != null)
                return Created("Patient Added", patient);
            else
                return BadRequest("Unable to fetch");
        }

        [HttpPut("Update Doctor Status")]

        public async Task<ActionResult<User>> UpdateDoctorStatus(User user)
        {
            var doctor = await _service.UpdateDoctor(user);
            if (doctor != null)
                return Created("Doctor Updated", doctor);
            else
                return BadRequest("Unable to fetch");
        }

        [HttpPost("Login")]
        
        public async Task<ActionResult<UserDTO>> Login(UserDTO userDTO) 
        {
            var doctor = await _service.Login(userDTO);
            if (doctor != null)
                return Created("Doctor Updated", doctor);
            else
                return BadRequest("Unable to fetch");
        }
    }
}
