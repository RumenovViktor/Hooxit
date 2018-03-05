using Models;
using System.ComponentModel.DataAnnotations;

namespace Hooxit.Models.Users
{
    public class Company
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
