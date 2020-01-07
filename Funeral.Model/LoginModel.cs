using System;
using System.ComponentModel.DataAnnotations;

namespace Funeral.Model
{
    public class LoginModel
    {
    }


    public class ForgotPassword
    {
        [Required(ErrorMessage = "please enter email")]
        public string Email { get; set; }
    }
    public class NewPassword
    {
        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Enter Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
        public string secureId { get; set; }

        public Guid ParlourId { get; set; }
        public int UserId { get; set; }
        public string ForgotId { get; set; }
    }
    public class ForgotPasswordModel
    {
        public Int64 PkiForgotId { get; set; }
        public int UserId { get; set; }
        public Guid ParlourId { get; set; }
        public bool IsChanged { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
