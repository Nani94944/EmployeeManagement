﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmployeeManagement.Models
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext ( DbContextOptions<EmployeeDbContext> options ) : base ( options )
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}