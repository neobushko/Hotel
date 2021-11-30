using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.PL.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Old password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "fill this!")]
        public string OldPassword { get; set; }

        [Display(Name = "New passsword")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "fill this!")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Required(ErrorMessage = "fill this!")]
        public string ConfirmNewPassword { get; set; }
    }
}
