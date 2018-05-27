using Hooxit.Presentation.Interfaces.Company;
using System.ComponentModel.DataAnnotations;

namespace Hooxit.Presentation.Implemenation.Company.Write
{
    public class ChangeWhatWeOfferDescription : IPresentationSegment
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
