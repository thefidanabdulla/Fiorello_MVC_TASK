using Microsoft.AspNetCore.Identity;

namespace Fiorello_MVC_TASK.Models
{
    public class User : IdentityUser
    {
        public string Fullname { get; set; }
    }
}
