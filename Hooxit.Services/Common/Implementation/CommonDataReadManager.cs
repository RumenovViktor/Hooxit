using System.Linq;
using System.Collections.Generic;

using Hooxit.Models;
using Hooxit.Data.Contracts;
using Hooxit.Services.Common.Interfaces;
using Hooxit.Presentation.Common;
using Hooxit.Presentation.Implementation;

namespace Hooxit.Services.Common.Implementation
{
    public class CommonDataReadManager : ICommonDataManager
    {
        private readonly IReadRepository<Country> countriesRepository;
        private readonly IReadByNameRepository<Skill> skillsRepository;

        public CommonDataReadManager(IUnitOfWork unitOfWork)
        {
            this.countriesRepository = unitOfWork.BuildCountriesRepository();
            this.skillsRepository = unitOfWork.BuildSkillsReadByNameRepository();
        }

        public IList<CountryReadModel> GetAllCountries()
        {
            return countriesRepository.GetAll().Select(x => new CountryReadModel(x)).ToList();
        }

        public IList<SkillReadModel> GetSkillsByName(string name)
        {
            return this.skillsRepository.GetByName(name).Select(x => new SkillReadModel(x)).ToList();
        }
    }
}