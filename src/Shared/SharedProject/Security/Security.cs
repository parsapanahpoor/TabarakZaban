namespace SharedProject.Security;

public static class SecurityUtils
{
    private static int HashCodeMultiplier => 21;

    public static int HashCodeSalter(int? baseCode)
    {
        return (baseCode ?? 0) * HashCodeMultiplier;
    }
}