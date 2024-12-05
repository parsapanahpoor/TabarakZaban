using Microsoft.AspNetCore.Mvc;

namespace WebApp.Presentation.Areas.Admin.ViewComponents;

public class AdminHeaderViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    => View("AdminHeader");
}
