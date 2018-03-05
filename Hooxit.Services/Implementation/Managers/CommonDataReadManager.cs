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

        public CommonDataReadManager(IUnitOfWork unitOfWork)
        {
            this.countriesRepository = unitOfWork.BuildCountriesRepository();
        }

        public IList<CountryReadModel> GetAllCountries()
        {
            return countriesRepository.GetAll().Select(x => new CountryReadModel(x)).ToList();
        }
    }
}