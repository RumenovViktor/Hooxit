using System.Collections.Generic;
using System.Threading.Tasks;
using Hooxit.Presentation.Implemenation;
using Hooxit.Presentation.Implemenation.Company.Write;
using Microsoft.AspNetCore.Http;

namespace Hooxit.Services.Company.Interfaces
{
    public interface ICompanyProfileService
    {
        Task<bool> ChangeDescription(ChangeCompanyDescriptionWrite changeComapnyDescription);
        Task<bool> ShowInterest(string userName);
        Task RemoveInterest(string userName);
        Task<bool> GetInterest(string userName);
        Task<IEnumerable<IdNameReadModel>> AllInterested();
        Task UploadPicture(IFormFile picture);
    }
}
