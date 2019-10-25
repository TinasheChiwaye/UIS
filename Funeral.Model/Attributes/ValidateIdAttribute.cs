using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Funeral.Model.Attributes
{
    public class ValidateIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool IDCheck = Funeral.Validation.IDValidation.IsValidID(value.ToString());
            
            if (IDCheck)
            {
                return ValidationResult.Success;
            }
            else {
                return new ValidationResult("Invalid Id Number.");
            }
        }
    }
}