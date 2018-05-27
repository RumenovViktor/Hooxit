using System.ComponentModel.DataAnnotations;

namespace Hooxit.Presentation.Implemenation.Company.Write
{
    public class ChangeCompanyDescriptionWrite
    {
        [Required]
        [MinLength(1)]
        public string CompanyDescription { get; set; }
    }
}
