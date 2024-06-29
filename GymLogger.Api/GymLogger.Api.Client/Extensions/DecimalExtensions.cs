namespace GymLogger.Api.Client.Extensions;

public static class DecimalExtensions
{
    public static string ToFixedString(this decimal? value)
    {
        return value.HasValue ? value.Value.ToString("0.##") : string.Empty;
    }
}
