using FluentValidation.Resources;
using System.Globalization;

namespace SharedProject.FluentValidationConfiguration;

public class CustomLanguageManager : LanguageManager
{
    public CustomLanguageManager() { }

    public override string GetString(string key, CultureInfo? culture = null)
    => base.GetString(key, Thread.CurrentThread.CurrentUICulture);
}
