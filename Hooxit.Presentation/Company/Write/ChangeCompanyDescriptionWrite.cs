using System.ComponentModel.DataAnnotations;

namespace Hooxit.Presentation.Company.Write
{
    public class ChangeCompanyDescriptionWrite
    {
        [Required]
        [MinLength(1)]
        public string CompanyDescription { get; set; }
    }
}
