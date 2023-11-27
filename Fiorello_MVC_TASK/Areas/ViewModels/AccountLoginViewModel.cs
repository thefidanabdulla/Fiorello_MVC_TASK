using System.ComponentModel.DataAnnotations;

namespace Fiorello_MVC_TASK.Areas.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required]
        public string  Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
