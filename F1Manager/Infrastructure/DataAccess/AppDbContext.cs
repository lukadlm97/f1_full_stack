﻿using Domain.ConstructorRacingDetails;
using Domain.Constructors;
using Domain.Contracts;
using Domain.Countries;
using Domain.DriverRoles;
using Domain.Drivers;
using Domain.Roles;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Constructor> Constructors { get; set; }
        public DbSet<ConstructorsRacingDetail> ConstructorsRacingDetails { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<DriverRoles> DriverRoles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(x => x.Country);
            modelBuilder.Entity<Driver>().HasOne(x => x.Country);
            modelBuilder.Entity<Driver>().HasMany<Contract>(x=>x.Contracts);
            modelBuilder.Entity<Constructor>().HasOne(x => x.Country);
            modelBuilder.Entity<Constructor>().HasMany<Contract>(x => x.Contracts);
            modelBuilder.Entity<ConstructorsRacingDetail>().HasOne(x => x.Constructor);
        }
    }
}