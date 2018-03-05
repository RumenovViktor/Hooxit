using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Windows.Input;

namespace Hooxit.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult ExecuteAction(ModelStateDictionary modelState, Func<IActionResult> action, IActionResult notValidResult)
        {
            try
            {
                if (modelState.IsValid)
                {
                    return action();
                }

                return notValidResult;
            }
            catch (Exception e)
            {
                throw new Exception("Something wrong happened.", e.InnerException);
            }
        }
    }
}
