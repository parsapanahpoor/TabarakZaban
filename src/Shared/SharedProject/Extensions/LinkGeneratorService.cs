using Microsoft.Extensions.Options;

namespace SharedProject.Extensions;

public class LinkGeneratorService
{
    private readonly ApplicationSettings _appSettings;

    public LinkGeneratorService(IOptions<ApplicationSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public string GenerateConfirmEmailLink(string endPointControllerName , string endPointAPIName , string userId, string token)
    {
        var uriBuilder = new UriBuilder(_appSettings.BaseUrl!)
        {
            Path = $"{endPointControllerName}/{endPointAPIName}",
            Query = $"{Uri.EscapeDataString(userId)}/{Uri.EscapeDataString(token)}"
        };

        return uriBuilder.ToString();
    }
}