using HospitalManagement.Context;
using HospitalManagement.Interface;
using HospitalManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
                transaction.CreateSavepointAsync("Add patients");
                _hospitalContext.Patients.Add(item);
                await _hospitalContext.SaveChangesAsync();
                transaction.Commit();
                return item;
            }
            catch (Exception)
            {
                transaction.RollbackToSavepoint("Add Doctor");
                throw new Exception();
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
                if (patient != null)
                {
                    return patient;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<ICollection<Patient>?> GetAll()
        {
            var doctors = await _hospitalContext.Patients.ToListAsync();
            if (doctors != null)
            {
                return doctors;
            }
            else
            {
                return null;
            }
        }

        public async Task<Patient?> Update(Patient item)
        {
            var patient = Get(item.Id);
            if (patient != null)
            {
                try
                {
                    _hospitalContext.Patients.Update(item);
                    await _hospitalContext.SaveChangesAsync();
                    return item;
                }
                catch (Exception err)
                {
                    Console.WriteLine("ERROR");
                    Console.WriteLine(err.Message);
                    Debug.WriteLine(err.Message);
                }
            }
            return null;
        }
    }
}
