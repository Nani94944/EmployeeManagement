using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.Validation
{
    public class NoFutureHireDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid ( object value , ValidationContext validationContext )
        {
            if (value is DateTime hireDate)
            {
                if (hireDate>DateTime.Now)
                {
                    return new ValidationResult ( ErrorMessage );
                }
            }
            return ValidationResult.Success;
        }
    }
}