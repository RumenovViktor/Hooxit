using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Models;
using Hooxit.Presentation.Implemenation;
using Hooxit.Presentation.Implemenation.Company.Write;
using Hooxit.Services.Company.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Hooxit.Services.Company.Implemenation
{
    public class ProfileService : ICompanyProfileService
    {
        private readonly ICompaniesRepository companiesRepository;
        private readonly IUserRepository userRepository;
        private readonly IRepository<CandidateInterest> candidateInterestRepository;
        private readonly IReadRepository<CandidateInterest> candidateInterestReadRepository;
        private readonly IDeleteRepository<CandidateInterest> candidateInterestDeleteRepository;
        private readonly ICandidateRepository candidatesRepository;

        public ProfileService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            companiesRepository = unitOfWork.BuildCompaniesRepository();
            candidateInterestRepository = unitOfWork.BuildCandidateInterestRepository();
            candidatesRepository = unitOfWork.BuildCandidateRepository();
            candidateInterestReadRepository = unitOfWork.BuildCandidateInterestReadRepository();
            candidateInterestDeleteRepository = unitOfWork.BuildCandidateInterestDeleteRepository();
        }

        public async Task<bool> ChangeDescription(ChangeCompanyDescriptionWrite changeComapnyDescription)
        {
            var user = await userRepository.GetByName(UserInfo.UserName);
            var company = companiesRepository.GetBydId(user.Id);

            try
            {
                company.CompanyDescription = changeComapnyDescription.CompanyDescription;
                companiesRepository.Save();

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> GetInterest(string userName)
        {
            var user = await userRepository.GetByName(UserInfo.UserName);
            var company = companiesRepository.GetBydId(user.Id);

            var candidateIdentity = await userRepository.GetByName(userName);
            var candidateId = candidatesRepository.GetById(candidateIdentity.Id).Id;

            var companyInterests = candidateInterestReadRepository.GetManyByIds(new[] { company.Id });

            if (companyInterests.Any(x => x.CandidateId == candidateId))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> ShowInterest(string userName)
        {
            var user = await userRepository.GetByName(UserInfo.UserName);
            var company = companiesRepository.GetBydId(user.Id);
            var candidateIdentity = await userRepository.GetByName(userName);
            var candidateId = candidatesRepository.GetById(candidateIdentity.Id).Id;

            candidateInterestRepository.Add(new CandidateInterest
            {
                CandidateId = candidateId,
                CompanyId = company.Id
            });

            return true;
        }

        public async Task RemoveInterest(string userName)
        {
            var user = await userRepository.GetByName(UserInfo.UserName);
            var company = companiesRepository.GetBydId(user.Id);

            var candidateInterests = candidateInterestReadRepository.GetManyByIds(new[] { company.Id }).ToList();
            candidateInterests.ForEach(x =>
            {
                var candidate = candidatesRepository.GetById(x.CandidateId);
                var candidateUser = userRepository.Get(candidate.UserId).Result;

                if (candidateUser.UserName.Equals(userName))
                {
                    candidateInterestDeleteRepository.Delete(x);
                }
            });

            candidateInterestRepository.Save();
        }

        public async Task<IEnumerable<IdNameReadModel>> AllInterested()
        {
            var user = await userRepository.GetByName(UserInfo.UserName);
            var company = companiesRepository.GetBydId(user.Id);

            var candidateIds = candidateInterestReadRepository.GetManyByIds(new[] { company.Id }).Select(x => x.CandidateId);
            var candidates = candidatesRepository.GetManyByIds(candidateIds.ToArray());

            return candidates.Select(x => new IdNameReadModel { Name = userRepository.Get(x.UserId).Result.UserName });
        }

        public async Task UploadPicture(IFormFile picture)
        {
            var user = await userRepository.GetByName(UserInfo.UserName);
            var company = companiesRepository.GetBydId(user.Id);

            if (picture != null && picture.Length > 0)
            {
                byte[] pictureBytes = null;

                using (var fileStream = picture.OpenReadStream())
                { 
                    using (var memoryStram = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStram);
                        pictureBytes = memoryStram.ToArray();
                    }
                }

                company.Picture = pictureBytes;
                companiesRepository.Update(company);
                companiesRepository.Save();
            }
        }
    }
}
