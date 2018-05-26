using Hooxit.Services.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hooxit.Components
{
    [ViewComponent]
    public class Countries : ViewComponent
    {
        private readonly ICommonDataManager commonDataReadManager;

        public Countries(ICommonDataManager commonDataReadManager)
        {
            this.commonDataReadManager = commonDataReadManager;
        }

        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult<IViewComponentResult>(View(commonDataReadManager.GetAllCountries()));
        }
    }
}