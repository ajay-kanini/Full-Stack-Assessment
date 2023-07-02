using HospitalManagement.Context;
using HospitalManagement.Interface;
using HospitalManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HospitalManagement.Service
{
    public class PatientRepo : IRepo<Patient, int>
    {
        private HospitalContext _hospitalContext;

        public PatientRepo(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
        }

        public async Task<Patient?> Add(Patient item)
        {
            var transaction = _hospitalContext.Database.BeginTransaction();
            try
            {
                await transaction.CreateSavepointAsync("Add patients");
                _hospitalContext.Patients.Add(item);
                await _hospitalContext.SaveChangesAsync();
                transaction.Commit();
                return item;
            }
            catch (Exception ex)
            {
                transaction.RollbackToSavepoint("Add Doctor");
                // Handle the exception or log the error
                Console.WriteLine($"Failed to add patient: {ex.Message}");
                return null;
            }
        }

        public Task<Patient?> Delete(Patient item)
        {
            throw new NotImplementedException();
        }

        public async Task<Patient?> Get(int key)
        {
            try
            {
                var patient = await _hospitalContext.Patients.FirstOrDefaultAsync(u => u.Id == key);
                return patient;
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                Console.WriteLine($"Failed to get patient: {ex.Message}");
                return null;
            }
        }

        public async Task<ICollection<Patient>?> GetAll()
        {
            try
            {
                var patients = await _hospitalContext.Patients.ToListAsync();
                return patients;
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                Console.WriteLine($"Failed to get all patients: {ex.Message}");
                return null;
            }
        }

        public async Task<Patient?> Update(Patient item)
        {
            try
            {
                var patient = await Get(item.Id);
                if (patient != null)
                {
                    _hospitalContext.Patients.Update(item);
                    await _hospitalContext.SaveChangesAsync();
                    return item;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                Console.WriteLine($"Failed to update patient: {ex.Message}");
                Debug.WriteLine($"Failed to update patient: {ex.Message}");
            }
            return null;
        }
    }
}
