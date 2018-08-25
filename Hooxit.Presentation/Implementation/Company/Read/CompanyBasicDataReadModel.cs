using Hooxit.Presentation.Implemenation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hooxit.Presentation.Implementation.Company.Read
{
    public class CompanyBasicDataReadModel
    {
        public IEnumerable<IdNameReadModel> IdsNames { get; set; }
        public string Picture { get; set; }
    }
}
