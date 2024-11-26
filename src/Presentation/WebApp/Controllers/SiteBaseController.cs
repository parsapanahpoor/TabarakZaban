using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApp.Presentation.Filter;
namespace WebApp.Controllers;

public abstract class SiteBaseController : Controller
{
    public static string SuccessMessage = "SuccessMessage";
    public static string ErrorMessage = "ErrorMessage";
    public static string InfoMessage = "InfoMessage";
    public static string WarningMessage = "WarningMessage";
}
