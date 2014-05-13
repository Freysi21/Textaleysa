using System.ComponentModel.DataAnnotations;

namespace Textaleysa.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Notandanafn")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Núverandi lykilorð")]
        public string OldPassword { get; set; }

        [Required]
		[StringLength(100, ErrorMessage = "{0} verður að vera minnst {2} stafir.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nýtt lykilorð")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Staðfesta lykilorða")]
        [Compare("NewPassword", ErrorMessage = "Nýja lykilorðið og staðfesting þess passa ekki")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Notandanafn")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lykilorð")]
        public string Password { get; set; }

        [Display(Name = "Muna eftir mér?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Notandanafn")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} verður að vera minnst {2} stafir.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lykilorð")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Staðfesta lykilorð")]
		[Compare("Password", ErrorMessage = "Nýja lykilorðið og staðfesting þess passa ekki")]
        public string ConfirmPassword { get; set; }
    }
}
