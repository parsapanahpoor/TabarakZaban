using System.Text.RegularExpressions;

namespace SharedProject.Extensions;

public static class StringExtensions
{
    public static string? ToSlug(this string? title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return string.Empty;

        title = title.ToLowerInvariant();

        title = Regex.Replace(title, @"[^a-z0-9\s-]", "");

        title = Regex.Replace(title, @"\s+", "-").Trim('-');

        title = Regex.Replace(title, @"-+", "-");

        return title;
    }
}
