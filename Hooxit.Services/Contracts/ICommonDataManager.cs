using Hooxit.Presentation.Read;
using System.Collections.Generic;

namespace Hooxit.Services.Contracts
{
    public interface ICommonDataManager
    {
        IList<CountryReadModel> GetAllCountries();

        IList<SkillReadModel> GetSkillsByName(string name);
    }
}
