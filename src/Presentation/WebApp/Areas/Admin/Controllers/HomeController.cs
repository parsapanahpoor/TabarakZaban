using Microsoft.AspNetCore.Mvc;
using WebApp.Admin.Controllers;

namespace WebApp.Areas.Admin.Controllers;

public class HomeController : AdminBaseController
{
    public IActionResult Index()
        => View();
}
