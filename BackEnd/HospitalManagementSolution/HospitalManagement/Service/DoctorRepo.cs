using HospitalManagement.Context;
using HospitalManagement.Interface;
using HospitalManagement.Models;
using HospitalManagement.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HospitalManagement.Service
{
    public class DoctorRepo : IRepo<Doctor, int>
    {
        private HospitalContext _hospitalContext;

        public DoctorRepo(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
        }

        public async Task<Doctor?> Add(Doctor item)
        {
            using (var transaction = _hospitalContext.Database.BeginTransaction())
            {
                if (_hospitalContext == null || _hospitalContext.Doctors == null)
                {
                    throw new Exception("Context is null");
                }
                try
                {
                    await transaction.CreateSavepointAsync("Add Doctor");
                    _hospitalContext.Doctors.Add(item);
                    await _hospitalContext.SaveChangesAsync();
                    transaction.Commit();
                    return item;
                }
                catch (Exception ex)
                {
                    transaction.RollbackToSavepoint("Add Doctor");
                    Debug.WriteLine($"Exception occurred while adding doctor: {ex}");
                    throw; // Rethrow the exception to be handled at a higher level
                }
            }
        }

        public async Task<Doctor?> Delete(Doctor item)
        {
            if(_hospitalContext == null || _hospitalContext.Doctors == null)
            {
                throw new Exception("Context is null");
            }
            try
            {
                _hospitalContext.Doctors.Remove(item);
                await _hospitalContext.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occurred while deleting doctor: {ex}");
                throw; // Rethrow the exception to be handled at a higher level
            }
        }

        public async Task<Doctor?> Get(int key)
        {
            if (_hospitalContext == null || _hospitalContext.Doctors == null)
            {
                throw new Exception("Context is null");
            }
            try
            {
                var doctor = await _hospitalContext.Doctors.FirstOrDefaultAsync(u => u.Id == key);
                if (doctor != null)
                {
                    return doctor;
                }
                else
                {
                    throw new InvalidOperationException($"Doctor with ID {key} not found"); // Throw an exception to indicate that the doctor was not found
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occurred while retrieving doctor: {ex}");
                throw; // Rethrow the exception to be handled at a higher level
            }
        }

        public async Task<ICollection<Doctor>?> GetAll()
        {
            if (_hospitalContext == null || _hospitalContext.Doctors == null)
            {
                throw new Exception("Context is null");
            }
            try
            {
                var doctors = await _hospitalContext.Doctors.ToListAsync();
                return doctors;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occurred while retrieving doctors: {ex}");
                throw; // Rethrow the exception to be handled at a higher level
            }
        }

        public async Task<Doctor?> Update(Doctor item)
        {
            try
            {
                var doctor = await Get(item.Id);
                if (doctor == null)
                    throw new InvalidOperationException($"Doctor with ID {item.Id} not found");

                if (doctor.Status == "Not Approved")
                {
                    doctor.Status = "Approved";
                }
                else if (doctor.Status == "Approved")
                {
                    doctor.Status = "Not Approved";
                }

                await _hospitalContext.SaveChangesAsync();
                return doctor;
            }
            catch (InvalidOperationException)
            {
                throw; // Rethrow the exception to be handled at a higher level
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occurred while updating doctor: {ex}");
                throw; // Rethrow the exception to be handled at a higher level
            }
        }
    }
}
