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
                _hospitalContext.Users.Add(item);
                await _hospitalContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return item;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Handle the exception or log the error
                Console.WriteLine($"Failed to add user: {ex.Message}");
                throw new Exception("Failed to add user", ex);
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
                var user = await _hospitalContext.Users.FirstOrDefaultAsync(u => u.Id == key);
                return user;
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                Console.WriteLine($"Failed to get user: {ex.Message}");
                throw new Exception("Failed to get user", ex);
            }
        }

        public async Task<ICollection<User>?> GetAll()
        {
            try
            {
                var users = await _hospitalContext.Users.ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                Console.WriteLine($"Failed to get all users: {ex.Message}");
                throw new Exception("Failed to get all users", ex);
            }
        }

        public async Task<User?> Update(User item)
        {
            try
            {
                var user = await Get(item.Id);
                if (user != null)
                {
                    _hospitalContext.Users.Update(item);
                    await _hospitalContext.SaveChangesAsync();
                    return item;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                Console.WriteLine($"Failed to update user: {ex.Message}");
                Debug.WriteLine($"Failed to update user: {ex.Message}");
            }
            return null;
        }
    }
}
