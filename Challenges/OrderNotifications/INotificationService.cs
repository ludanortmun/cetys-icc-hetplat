using OrderNotifications.Models;

namespace OrderNotifications
{

    /// <summary>
    /// Centralized service for sending notifications about order status updates.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Sends a notification to the customer about the current status of their order.
        /// The notification channel can vary based on the customer's preferences and location.
        /// </summary>
        /// <param name="order"></param>
        void NotifyOrderStatus(Order order);
    }
}