namespace SharedProject.Extensions;

public static class EnumExtensions
{
    public static string? GetEnumName<TEnum>(this TEnum enumValue) 
        where TEnum : 
        struct, 
        Enum
        => Enum.GetName(typeof(TEnum), enumValue);
}
