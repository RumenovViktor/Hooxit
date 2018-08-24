using Hooxit.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hooxit.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public int CompanyID { get; set; }

        public Company Company { get; set; }
    }
}
