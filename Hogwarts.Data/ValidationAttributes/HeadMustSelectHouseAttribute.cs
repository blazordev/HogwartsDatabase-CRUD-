using Hogwarts.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Hogwarts.Data.ValidationAttributes
{
    //this is already implemented in staffController CreateStaff so no need to add attribute
    public class HeadMustSelectHouseAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var staff = (StaffDto)validationContext.ObjectInstance;

            if (staff.Roles.Any(r => r.Id == 6) && staff.HouseId == 0)
            {
                return new ValidationResult("House Head must select House",
                    new[] { nameof(StaffDto) });
            }

            return ValidationResult.Success;
        }
    }
}
