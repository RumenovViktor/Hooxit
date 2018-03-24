using Hooxit.Data.Contracts;
using Hooxit.Models;
using Hooxit.Presentation;
using Hooxit.Services.Implementation.Company.Interfaces;
using System.Collections.Generic;
using System.Linq;

// TODO: Get from company info when Lazy loading is available.
namespace Hooxit.Services.Implementation.Company.Implemenation
{
    public class PositionsManager : IPositionsManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Position> poisitionsRepository;

        public PositionsManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            this.poisitionsRepository = this.unitOfWork.BuildPositionsRepository();
        }

        public IEnumerable<IdNameReadModel> GetAll(int id)
        {
            var allPositions = this.poisitionsRepository.All();

            return allPositions.Where(x => x.CompanyID == id).Select(x => new IdNameReadModel
            {
                Id = x.PositionID,
                Name = x.PositionName
            }).ToList();
        }
    }
}
