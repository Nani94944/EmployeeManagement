using System;
using System.ComponentModel.DataAnnotations;
using DocumentFormat.OpenXml.Bibliography;
using EmployeeManagement.Models.Validation;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required ( ErrorMessage = "Name is required" )]
        [StringLength ( 100 , ErrorMessage = "Name cannot exceed 100 characters" )]
        public string? Name { get; set; }

        [Required ( ErrorMessage = "Email is required" )]
        [EmailAddress ( ErrorMessage = "Invalid email format" )]
        public string? Email { get; set; }

        [Required ( ErrorMessage = "Department is required" )]
        public Department? Department { get; set; }

        [Required ( ErrorMessage = "Hire date is required" )]
        [NoFutureHireDate ( ErrorMessage = "Hire date cannot be in the future" )]
        public DateTime HireDate { get; set; }

        [Required ( ErrorMessage = "Salary is required" )]
        [Range ( 0.01 , double.MaxValue , ErrorMessage = "Salary must be greater than 0" )]
        public decimal Salary { get; set; }
    }
}
