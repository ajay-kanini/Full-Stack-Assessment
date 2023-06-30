using HospitalManagement.Context;
using HospitalManagement.Interface;
using HospitalManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Service
{
    public class UserRepo : IRepo<User, int>
    {
        private HospitalContext _hospitalContext;

        public UserRepo(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
        }
        public async Task<User?> Add(User item)
        {

            var transaction = _hospitalContext.Database.BeginTransaction();
            try
            {
                transaction.CreateSavepointAsync("Add Users");
                _hospitalContext.Users.Add(item);
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

        public Task<User?> Delete(User item)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> Get(int key)
        {
            try
            {
                var patient = await _hospitalContext.Users.FirstOrDefaultAsync(u => u.Id == key);
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

        public async Task<ICollection<User>?> GetAll()
        {
            var users = await _hospitalContext.Users.ToListAsync();
            if (users != null)
            {
                return users;
            }
            else
            {
                return null;
            }
        }

        public async Task<User?> Update(User item)
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
