using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class User : IdentityUser
    {
        public virtual string Username
        {
            get
            {
                return base.UserName;
            }
        }

        public virtual string Password { get; set; }
    }
}
