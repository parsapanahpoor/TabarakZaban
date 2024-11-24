using Microsoft.AspNetCore.Mvc;
namespace WebHost.ViewComponents;

public class HeaderViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken)
        => View("Header");
}
