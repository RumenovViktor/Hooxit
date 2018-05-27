using System.Collections.Generic;

using Hooxit.Presentation.Common;
using Hooxit.Presentation.Implementation;

namespace Hooxit.Services.Common.Interfaces
{
    public interface ICommonDataManager
    {
        IList<CountryReadModel> GetAllCountries();

        IList<SkillReadModel> GetSkillsByName(string name);
    }
}
