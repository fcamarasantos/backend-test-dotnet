using System.Diagnostics.CodeAnalysis;

namespace ParkingLotManager.WebApi.Extensions;

public static class StringExtension
{
    public static bool IsNullOrEmptyOrWhiteSpace([NotNullWhen(false)] this string? value)
    {
        if (value?.Trim() == "")
            return true;
        if(string.IsNullOrEmpty(value))
            return true;
        return false;
    }
}
