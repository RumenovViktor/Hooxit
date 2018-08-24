using Hooxit.Models;

namespace Hooxit.Presentation.Implementation.Company.Read
{
    public class ProductReadModel
    {
        public ProductReadModel(Product source)
        {
            this.ProductID = source.ID;
            this.Url = source.Url;
            this.Description = source.Description;
            this.Name = source.Name;
        }

        public int ProductID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }
    }
}
