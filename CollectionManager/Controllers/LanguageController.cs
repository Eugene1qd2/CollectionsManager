using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.Controllers
{
    public class LanguageController : Controller
    {
        public IActionResult ChangeLanguage(string culture,string returnUrl)
        {
            HttpContext.Session.SetString("CurrentCulture", culture);
            return Redirect(returnUrl);
        }
    }
}
