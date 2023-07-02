using HospitalManagement.Interface;
using HospitalManagement.Models;
using HospitalManagement.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO?>> RegisterDoctor(DoctorDTO doctorDTO)
        {
            try
            {
                var doctor = await _service.DoctorRegistration(doctorDTO);
                if (doctor != null)
                    return Created("Doctor Added", doctor);
                else
                    return BadRequest("Unable to fetch");
            }
            catch (Exception ex)
            {
                // Log the exception or perform any other necessary actions
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred: " + ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO?>> RegisterPatient(PatientDTO patientDTO)
        {
            try
            {
                var patient = await _service.PatientRegistration(patientDTO);
                if (patient != null)
                    return Created("Patient Added", patient);
                else
                    return BadRequest("Unable to fetch");
            }
            catch (Exception ex)
            {
                // Log the exception or perform any other necessary actions
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred: " + ex.Message);
            }
        }

        [Authorize(Roles="Admin")]

        [HttpPut]
        [ProducesResponseType(typeof(Doctor), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Doctor>> UpdateDoctorStatus(Doctor doctor)
        {
            try
            {
                var doctors = await _service.UpdateDoctor(doctor);
                if (doctors != null)
                    return Created("Doctor Updated", doctors);
                else
                    return BadRequest("Unable to fetch");
            }
            catch (Exception ex)
            {
                // Log the exception or perform any other necessary actions
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred: " + ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> Login(UserDTO userDTO)
        {
            try
            {
                var doctor = await _service.Login(userDTO);
                if (doctor != null)
                    return Created("Doctor Updated", doctor);
                else
                    return BadRequest("Unable to fetch");
            }
            catch (Exception ex)
            {
                // Log the exception or perform any other necessary actions
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred: " + ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(Doctor), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Doctor>> GetDoctorDetails()
        {
            try
            {
                var doctor = await _service.GetAllDoctors();
                if (doctor != null)
                    return Ok(doctor);
                else
                    return BadRequest("Unable to fetch");
            }
            catch (Exception ex)
            {
                // Log the exception or perform any other necessary actions
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred: " + ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(Doctor), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Doctor>> GetOneDoctor(int key)
        {
            try
            {
                var doctor = await _service.GetDoctor(key);
                if (doctor != null)
                    return Ok(doctor);
                else
                    return BadRequest("Unable to fetch");
            }
            catch (Exception ex)
            {
                // Log the exception or perform any other necessary actions
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred: " + ex.Message);
            }
        }
    }
}
