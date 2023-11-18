using System.ComponentModel.DataAnnotations;

namespace Fiorello_MVC_TASK.ViewModels.Accounts
{
    public class AccountRegisterViewModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password)), Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}
