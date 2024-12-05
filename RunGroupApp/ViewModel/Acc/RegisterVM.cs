using System.ComponentModel.DataAnnotations;

namespace RunGroupApp.ViewModel.Acc
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Email Address is reuired")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is reuired")]
        [DataType(DataType.Password)]     
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm Password is reuired")]
        [Display(Name = "Confirm Password")]
        [Compare (nameof(ConfirmPassword), ErrorMessage = "Confirm Password is do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
