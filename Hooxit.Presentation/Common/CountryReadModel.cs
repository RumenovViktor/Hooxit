using Hooxit.Models;

namespace Hooxit.Presentation.Common
{
    public class CountryReadModel
    {
        public CountryReadModel() { }

        public CountryReadModel(Country source)
        {
            this.Id = source.Id;
            this.Name = source.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
