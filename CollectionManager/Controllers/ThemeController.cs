using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.Controllers
{
    public class ThemeController : Controller
    {
        public IActionResult ChangeTheme(bool theme,string returnUrl)
        {
            Console.WriteLine(theme);
            HttpContext.Session.SetString("CurrentTheme", theme ? "dark": "light");
            return Redirect(returnUrl);
        }
    }
}
