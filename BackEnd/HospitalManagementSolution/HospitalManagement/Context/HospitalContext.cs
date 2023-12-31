﻿using HospitalManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Context
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<Doctor>? Doctors { get; set; } 
        public DbSet<Patient>? Patients { get; set; }   
        public DbSet<User>? Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.Mail })
                .IsUnique(true);
        }
    }
}
