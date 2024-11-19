namespace SharedProject.Security;

public class GuidValidation
{
    public static bool IsValidGuid(string? guid)
        => Guid.TryParse(guid, out _);
}
