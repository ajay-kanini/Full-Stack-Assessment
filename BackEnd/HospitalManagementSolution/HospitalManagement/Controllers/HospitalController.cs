using HospitalManagement.Interface;
using HospitalManagement.Models;
using HospitalManagement.Models.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class HospitalController : ControllerBase
    {
        private IManageUsers _service;

        public HospitalController(IManageUsers service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO?>> RegisterDoctor(DoctorDTO doctorDTO)
        {
            var doctor = await _service.DoctorRegistration(doctorDTO);
            if (doctor != null)
                return Created("Doctor Added", doctor);
            else
                return BadRequest("Unable to fetch");
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO?>> RegisterPatient(PatientDTO patientDTO)
        {
            var patient = await _service.PatientRegistration(patientDTO);
            if (patient != null)
                return Created("Patient Added", patient);
            else
                return BadRequest("Unable to fetch");
        }

        [HttpPut]
        public async Task<ActionResult<Doctor>> UpdateDoctorStatus(Doctor doctor)
        {
            var doctors = await _service.UpdateDoctor(doctor);
            if (doctors != null)
                return Created("Doctor Updated", doctors);
            else
                return BadRequest("Unable to fetch");
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Login(UserDTO userDTO) 
        {
            var doctor = await _service.Login(userDTO);
            if (doctor != null)
                return Created("Doctor Updated", doctor);
            else
                return BadRequest("Unable to fetch");
        }

        [HttpGet]
        public async Task<ActionResult<Doctor>> GetDoctorDetails()
        {
            var doctor = await _service.GetAllDoctors();
            if (doctor != null)
                return Ok(doctor);
            else
                return BadRequest("Unable to fetch");
        }
    }
}
