namespace GymLogger.Api.Client.Extensions;

public static class DecimalExtensions
{
    public static string ToFixedString(this decimal? value)
    {
        return value.HasValue ? value.Value.ToString("0.##") : string.Empty;
    }

    public static string ToFixedKg(this decimal? value)
    {
        return value.HasValue ? value.Value.ToString("0.## kg") : string.Empty;
    }

    public static string ToFixedTime(this decimal? value)
    {
        return value.HasValue ? value.Value.ToString("0.## s") : string.Empty;
    }
}

public static class DateExtensions
{
    public static string ToShortDateString(this DateTime? value)
    {
        return value.HasValue ? value.Value.ToShortDateString() : string.Empty;
    }

    public static string ToCroDate(this DateOnly value)
    {
        return value.ToString("dd.MM.yyyy");
    }
}
