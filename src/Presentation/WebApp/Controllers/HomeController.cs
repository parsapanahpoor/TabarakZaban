using Account.Controllers;
using Microsoft.AspNetCore.Mvc;
using WebApp.HttpManager;

namespace WebApp.Controllers;

public class HomeController : SiteBaseController
{
	[HttpGet("LoginRegister")]
	[RedirectHomeIfLoggedInActionFilter]
	public IActionResult LoginRegister(LoginRegisterDto model)
	=> View(model);
}
