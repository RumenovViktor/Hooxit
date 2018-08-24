using Hooxit.Presentation.Implementation.Company.Read;
using System.Collections.Generic;

namespace Hooxit.Presentation.Implemenation.Company.Read
{
    public class ProfileInfoRead
    {
        public int CreatedPositions { get; set; }

        public string CompanyDescription { get; set; }

        public IEnumerable<ProductReadModel> Products { get; set; }

        public int InterestedInCount { get; set; }
    }
}
