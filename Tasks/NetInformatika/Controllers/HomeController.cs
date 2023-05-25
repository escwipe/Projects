using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetInformatika.Controllers;

[Authorize]
public class HomeController : Controller
{
    #region Pages
    public IActionResult Index()
    {
        return View();
    }
    #endregion
}