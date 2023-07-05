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
        private readonly HospitalContext _hospitalContext;

        public PatientRepo(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
        }

        public async Task<Patient?> Add(Patient item)
        {
            var transaction = _hospitalContext.Database.BeginTransaction();
            if (_hospitalContext == null || _hospitalContext.Patients == null)
            {
                throw new Exception("Context is null");
            }
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

        public async Task<Patient?> Delete(Patient item)
        {
            if (_hospitalContext == null || _hospitalContext.Patients == null)
            {
                throw new Exception("Context is null");
            }
            try
            {
                var patient = await Get(item.Id);
                if (patient != null)
                {
                    _hospitalContext.Patients.Remove(item);
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

        public async Task<Patient?> Get(int key)
        {
            if (_hospitalContext == null || _hospitalContext.Patients == null)
            {
                throw new Exception("Context is null");
            }
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
            if (_hospitalContext == null || _hospitalContext.Patients == null)
            {
                throw new Exception("Context is null");
            }
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
            if (_hospitalContext == null || _hospitalContext.Patients == null)
            {
                throw new Exception("Context is null");
            }
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
