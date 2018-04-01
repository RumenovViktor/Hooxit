using Hooxit.Services.Contracts;
using System.Collections.Generic;
using Hooxit.Presentation.Read;
using Hooxit.Models;
using Hooxit.Data.Contracts;
using System.Linq;

namespace Hooxit.Services.Implementation.Managers
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