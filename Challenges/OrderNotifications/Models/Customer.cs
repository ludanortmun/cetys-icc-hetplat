namespace OrderNotifications.Models
{
    public class Customer
    {
        /// <summary>
        /// Unique identifier for the customer
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ISO 3166-1 alpha-2 country code (e.g., "US" for United States, "GB" for United Kingdom)
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// The name of the customer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Represents the contact information for the customer
        /// </summary>
        public ContactInfo ContactInfo { get; set; }

        public Dictionary<NotificationChannel, NotificationChannelPreference> Preferences { get; set; } = new();
    }
}
