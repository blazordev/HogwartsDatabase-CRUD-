using Hogwarts.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.ValidationAttributes
{
    public class NameMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var role = (RoleForCreationDto)validationContext.ObjectInstance;

            if (role.Name == role.Description)
            {
                return new ValidationResult("Name must be different from Description",
                    new[] { nameof(RoleForCreationDto) });
            }

            return ValidationResult.Success;
        }
    }
}
