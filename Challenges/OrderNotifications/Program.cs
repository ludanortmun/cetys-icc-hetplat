using OrderNotifications.Models;

namespace OrderNotifications
{
    public class Program
    {
        static void Main(string[] args)
        {
            INotificationService notificationService = new NotificationService();

            var order = new Order
            {
                Id = Guid.NewGuid(),
                Status = OrderStatus.Processing,
                Customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "John Doe",
                    CountryCode = "US",
                    ContactInfo = new ContactInfo
                    {
                        Email = "test@example.com",
                        PhoneNumber = "123-456-7890"
                    },
                },
            };

            Console.WriteLine("Should send notification via Email and SMS");
            notificationService.NotifyOrderStatus(order);

            var anotherOrder = new Order
            {
                Id = Guid.NewGuid(),
                Status = OrderStatus.Shipped,
                Customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Juan Pérez",
                    CountryCode = "MX",
                    ContactInfo = new ContactInfo
                    {
                        Email = "juan@dominio.mx",
                        PhoneNumber = "987-654-3210",
                    },
                }
            };

            Console.WriteLine("Should send notification via Email and WhatsApp");
            notificationService.NotifyOrderStatus(anotherOrder);

            var akOrder = new Order
            {
                Id = Guid.NewGuid(),
                Status = OrderStatus.Processing,
                Customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Dimitri Petrovski",
                    CountryCode = "AK",
                    ContactInfo = new ContactInfo
                    {
                        Email = "me@arstotzka.ak",
                        PhoneNumber = "555-555-5555",
                    },
                }
            };
            Console.WriteLine("Should send notification via Email only");
            notificationService.NotifyOrderStatus(akOrder);

            var mxOrderWithSmsPreference = new Order
            {
                Id = Guid.NewGuid(),
                Status = OrderStatus.Delivered,
                Customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Ana Gómez",
                    CountryCode = "MX",
                    ContactInfo = new ContactInfo
                    {
                        Email = "ana@dominio.mx",
                        PhoneNumber = "222-333-4444",
                    },
                    Preferences = new Dictionary<NotificationChannel, NotificationChannelPreference>
                    {
                        { NotificationChannel.SMS, NotificationChannelPreference.Enabled }
                    }
                }
            };

            Console.WriteLine("Should send notification via Email, WhatsApp and SMS ");
            notificationService.NotifyOrderStatus(mxOrderWithSmsPreference);
        }
    }
}
