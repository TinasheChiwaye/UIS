using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Funeral.Web.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateDateAttribute : ValidationAttribute
    {
        public ValidateDateAttribute(string errorMessage) : base(errorMessage) { }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            DateTime? dateTime = value as DateTime?;

            if (dateTime.HasValue && dateTime == new DateTime(1900, 12, 31))
                return false;

            return true;
        }
    }
}