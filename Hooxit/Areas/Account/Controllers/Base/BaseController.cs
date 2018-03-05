using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Hooxit.Account.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult ExecuteAction(ModelStateDictionary modelState, Func<bool, object> action)
        {
            try
            {
                if (modelState.IsValid)
                {
                    //return action();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }
    }
}
