using Microsoft.AspNetCore.Mvc;

namespace WebApp.Presentation.Areas.Admin.ViewComponents;

public class AdminSideBarViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    =>View("AdminSideBar");
}
