using Hooxit.Presentation.Read;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hooxit.Services.Contracts
{
    public interface ICommonDataManager
    {
        IList<CountryReadModel> GetAllCountries();
    }
}
