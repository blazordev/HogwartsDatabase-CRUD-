using Hogwarts.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Hogwarts.Data.ValidationAttributes
{
    public class HeadMustSelectHouseAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var staff = (StaffForCreationDto)validationContext.ObjectInstance;

            if (staff.Roles.Any(r => r.Id == 6) && staff.House == null)
            {
                return new ValidationResult("House Head must select House",
                    new[] { nameof(StaffForCreationDto) });
            }

            return ValidationResult.Success;
        }
    }
}
