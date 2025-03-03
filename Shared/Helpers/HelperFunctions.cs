namespace Shared.Helpers;

public static class HelperFunctions
{
    public static string? CapitalizeFirst(string? str)
    {
        if (str == null) return null;
        return str[..1].ToUpper() + str[1..];
    }
}