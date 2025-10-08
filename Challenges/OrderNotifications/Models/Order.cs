namespace OrderNotifications.Models
{
    public class Order
    {
        /// <summary>
        /// The unique identifier for the order
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The current status of the order
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// The customer who placed the order
        /// </summary>
        public Customer Customer { get; set; }
    }
}
