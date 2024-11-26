using Microsoft.AspNetCore.Mvc;
using WebApp.HttpManager;

namespace WebApp.Controllers;

public class HomeController : SiteBaseController
{
	public IActionResult Index()
		=> View();
}
