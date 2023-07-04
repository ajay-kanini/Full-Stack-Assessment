using Microsoft.EntityFrameworkCore;
using HospitalManagement.Context;
using HospitalManagement.Interface;
using HospitalManagement.Models;
using HospitalManagement.Service;

namespace UnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]

        public DbContextOptions<HospitalContext> GetDbcontextOption()
        {
            var contextOptions = new DbContextOptionsBuilder<HospitalContext>()
                                    .UseInMemoryDatabase(databaseName: "userInMemory")
                                    .Options;
            return contextOptions;
        }

        [TestMethod]
        public async Task TestGetUser()
        {
            using (var userContext = new HospitalContext(GetDbcontextOption()))
            {
                userContext.Patients.Add(new Patient
                {
                    Name = "Gimu G",
                    PhoneNumber = "9876543210",
                    DateOfBirth = new DateTime(2001, 02, 14),
                    Age = 22,
                    Address = "Erode, TamilNadu",
                    Users = new User() { PasswordHash = new byte[] { }, PasswordKey = new byte[] { }, Mail = "summa.kanini@gmail.com", Role = "Intern" },
                });
                await userContext.SaveChangesAsync();
                             
            }
            using (var userContext = new HospitalContext(GetDbcontextOption()))
            {
                IRepo<Patient, int> repo = new PatientRepo(userContext);
                var data = await repo.GetAll();
                Assert.AreEqual(1, data.ToList().Count);
            }
            Assert.IsFalse(false);
        }
    }
}