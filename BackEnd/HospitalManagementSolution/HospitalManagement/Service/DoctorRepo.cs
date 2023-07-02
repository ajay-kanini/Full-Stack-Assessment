using HospitalManagement.Context;
using HospitalManagement.Interface;
using HospitalManagement.Models;
using HospitalManagement.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
            var transaction = _hospitalContext.Database.BeginTransaction(); 
            try
            {
                await transaction.CreateSavepointAsync("Add Doctor");
                _hospitalContext.Doctors.Add(item);
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

        public Task<Doctor> Delete(Doctor item)
        {
            throw new NotImplementedException();
        }

        public async Task<Doctor> Get(int key)
        {
            try
            {
                var doctor = await _hospitalContext.Doctors.FirstOrDefaultAsync(u => u.Id == key);
                if(doctor != null)
                {
                    return doctor;
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

        public async Task<ICollection<Doctor>> GetAll()
        {
            var doctors = await _hospitalContext.Doctors.ToListAsync();
            if(doctors != null)
            {
                return doctors;
            }
            else
            {
                return null;
            }
        }

        public async Task<Doctor> Update(Doctor item)
        {
            var user = await Get(item.Id);
            if (user == null)
                return null;
            else if (user.Status == "Not Approved")
            {
                user.Status = "Approved";
            }
            else if (user.Status == "Approved")
            {
                user.Status = "Not Approved";
            }
            await _hospitalContext.SaveChangesAsync();
            return user;
        }
    }
}
