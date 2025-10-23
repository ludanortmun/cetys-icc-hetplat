namespace Cookies.Models
{
    public class UserPreference
    {
        public string? DisplayName { get; set; }

        public DateFormat PreferredDateFormat { get; set; } = DateFormat.YYYYMMDD;
    }

    public enum DateFormat
    {
        MMDDYYYY,
        DDMMYYYY,
        YYYYMMDD
    }
}
