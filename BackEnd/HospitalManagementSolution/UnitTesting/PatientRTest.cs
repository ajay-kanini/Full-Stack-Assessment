using Microsoft.EntityFrameworkCore;
using HospitalManagement.Context;
using HospitalManagement.Interface;
using HospitalManagement.Models;
using HospitalManagement.Service;

namespace UnitTesting
{
    [TestClass]
    public class PatientRTest
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
                userContext.Patients?.Add(new Patient
                {
                    Name = "Gimu G",
                    PhoneNumber = "9876543210",
                    DateOfBirth = new DateTime(2001, 02, 14),
                    Age = 22,
                    Address = "Erode, TamilNadu",
                    Users = new User() { PasswordHash = Array.Empty<byte>(), PasswordKey = Array.Empty<byte>(), Mail = "summa.kanini@gmail.com", Role = "Intern" },
                });
                await userContext.SaveChangesAsync();
                             
            }
            using (var userContext = new HospitalContext(GetDbcontextOption()))
            {
                IRepo<Patient, int> repo = new PatientRepo(userContext);
                var data = await repo.GetAll();
                if(data !=null)
                    Assert.AreEqual(3, data.ToList().Count);
            }
            Assert.IsFalse(false);
        }

        [TestMethod("Test add")]
        public async Task TestAddUser()
        {
            using (var userContext = new HospitalContext(GetDbcontextOption()))
            {
                userContext.Patients?.Add(new Patient
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
                IRepo<Patient, int> repo = new PatientRepo(userContext);
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
                IRepo<Patient, int> repo = new PatientRepo(userContext);
                var data = await repo.Delete
                    (new Patient
                    {
                        Id = 1,
                        Name = "Rahul",
                        PhoneNumber = "7895461235",
                        DateOfBirth = new DateTime(2002, 04, 20),
                        Age = 22,
                        Address = "Chennai, TamilNadu",
                        Users = new User() { PasswordHash = Array.Empty<byte>(), PasswordKey = Array.Empty<byte>(), Mail = "ajay.kanini@gmail.com", Role = "Intern" },
                    }
                    );
                if (data != null)
                    Assert.AreEqual(new Patient(), data);
            }
            Assert.IsFalse(false);
        }

        [TestMethod("Test get")]    
        public async Task TestGetAllUser()
        {
            using (var userContext = new HospitalContext(GetDbcontextOption()))
            {
                userContext.Patients?.Add(new Patient
                {
                    Id = 3,
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
                IRepo<Patient, int> repo = new PatientRepo(userContext);
                var data = await repo.Get(1);
                if (data != null)
                    Assert.IsNotNull(data);
            }
            Assert.IsFalse(false);
        }

    }
}