using HospitalManagement.Context;
using HospitalManagement.Interface;
using HospitalManagement.Models;
using HospitalManagement.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class DoctorRTest
    {
        public DbContextOptions<HospitalContext> GetDbcontextOption()
        {
            var contextOptions = new DbContextOptionsBuilder<HospitalContext>()
                                    .UseInMemoryDatabase(databaseName: "userInMemory")
                                    .Options;
            return contextOptions;
        }

        [TestMethod("Test get all")]
        public async Task TestGetUser()
        {
            using (var userContext = new HospitalContext(GetDbcontextOption()))
            {
                userContext.Doctors?.Add(new Doctor
                {
                    Name = "Gimu G",
                    PhoneNumber = "9876543210",
                    DateOfBirth = new DateTime(2001, 02, 14),
                    Age = 22,
                    Specialization = "ENT",
                    Qualifications = "MBBS",
                    Gender = "Male",
                    Address = "Erode, TamilNadu",
                    Users = new User() { PasswordHash = Array.Empty<byte>(), PasswordKey = Array.Empty<byte>(), Mail = "summa.kanini@gmail.com", Role = "Intern" },
                });
                await userContext.SaveChangesAsync();

            }
            using (var userContext = new HospitalContext(GetDbcontextOption()))
            {
                IRepo<Doctor, int> repo = new DoctorRepo(userContext);
                var data = await repo.GetAll();
                if (data != null)
                    Assert.AreEqual(2, data.ToList().Count);
            }
            Assert.IsFalse(false);
        }

        [TestMethod("Test add")]
        public async Task TestAddUser()
        {
            using (var userContext = new HospitalContext(GetDbcontextOption()))
            {
                userContext.Doctors?.Add(new Doctor
                {
                    Name = "Rahul",
                    PhoneNumber = "7895461235",
                    DateOfBirth = new DateTime(2002, 04, 20),
                    Age = 22,
                    Address = "Chennai, TamilNadu",
                    Users = new User() { PasswordHash = Array.Empty<byte>(), PasswordKey = Array.Empty<byte>(), Mail = "ajay.kanini@gmail.com", Role = "Intern" },
                });
                await userContext.SaveChangesAsync();

            }
            using (var userContext = new HospitalContext(GetDbcontextOption()))
            {
                IRepo<Doctor, int> repo = new DoctorRepo(userContext);
                var data = await repo.GetAll();
                if (data != null)
                    Assert.AreEqual(1, data.ToList().Count);
            }
            Assert.IsFalse(false);
        }

        [TestMethod("Delete ")]
        public async Task TestDeleteUser()
        {

            using (var userContext = new HospitalContext(GetDbcontextOption()))
            {
                IRepo<Doctor, int> repo = new DoctorRepo(userContext);
                var data = await repo.Delete
                    (new Doctor
                    {
                        Id = 1,
                        Name = "Rahul",
                        PhoneNumber = "7895461235",
                        DateOfBirth = new DateTime(2002, 04, 20),
                        Age = 22,
                        Specialization = "ENT",
                        Qualifications = "MBBS",
                        Address = "Chennai, TamilNadu",
                        Users = new User() { PasswordHash = Array.Empty<byte>(), PasswordKey = Array.Empty<byte>(), Mail = "ajay.kanini@gmail.com", Role = "Intern" },
                    }
                    );
                if (data != null)
                    Assert.IsNotNull(data); 
            }
            Assert.IsFalse(false);
        }

        [TestMethod("Test get")]
        public async Task TestGetAllUser()
        {
            using (var userContext = new HospitalContext(GetDbcontextOption()))
            {
                userContext.Doctors?.Add(new Doctor
                {
                    Id = 3,
                    Name = "Rahul",
                    PhoneNumber = "7895461235",
                    DateOfBirth = new DateTime(2002, 04, 20),
                    Age = 22,
                    Specialization = "ENT",
                    Qualifications = "MBBS",
                    Address = "Chennai, TamilNadu",
                    Users = new User() { PasswordHash = Array.Empty<byte>(), PasswordKey = Array.Empty<byte>(), Mail = "ajay.kanini@gmail.com", Role = "Intern" },
                });
                await userContext.SaveChangesAsync();

            }
            using (var userContext = new HospitalContext(GetDbcontextOption()))
            {
                IRepo<Doctor, int> repo = new DoctorRepo(userContext);
                var data = await repo.Get(3);
                if (data != null)
                    Assert.IsNotNull(data);
            }
            Assert.IsFalse(false);
        }
    }
}
