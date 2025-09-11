using DiscountCodes.Models;

namespace DiscountCodes.ConsoleRunner
{
    internal class Program
    {
        private static Product _testProduct1 = new("Brand1", "Product1", 10.99m);
        private static Product _testProduct2 = new("Brand2", "Product2", 20.50m);
        private static Product _testProduct3 = new("Brand2", "Product3", 30.00m);

        private static (string, Product[], decimal)[] _testCases =
        [
            ("BOGOFREE", [_testProduct1, _testProduct1 ], _testProduct1.Price),
            ("BOGOFREE", [_testProduct1, _testProduct1, _testProduct2, _testProduct2], _testProduct1.Price + _testProduct2.Price),
            ("BOGOFREE", [_testProduct1, _testProduct1, _testProduct2], _testProduct1.Price + _testProduct2.Price),
            ("BOGOFREE", [_testProduct1, _testProduct1, _testProduct1], _testProduct1.Price * 2),
            ("BRAND2DISCOUNT", [_testProduct2, _testProduct3], (_testProduct2.Price + _testProduct3.Price) * 0.9m),
            ("BRAND2DISCOUNT", [_testProduct1, _testProduct2, _testProduct3], (_testProduct1.Price + (_testProduct2.Price + _testProduct3.Price) * 0.9m)),
            ("10PERCENTOFF", [_testProduct1, _testProduct2], (_testProduct1.Price + _testProduct2.Price) * 0.9m),
            ("10PERCENTOFF", [_testProduct1, _testProduct2, _testProduct3], (_testProduct1.Price + _testProduct2.Price + _testProduct3.Price) * 0.9m),
            ("BRAND1MANIA", [_testProduct1, _testProduct1, _testProduct2], (_testProduct1.Price * 2 * 0.5m) + _testProduct2.Price),
            ("BRAND1MANIA", [_testProduct1, _testProduct1, _testProduct1], (_testProduct1.Price * 3 * 0.5m)),
            ("", [_testProduct1, _testProduct2], _testProduct1.Price + _testProduct2.Price),
            ("", [_testProduct1, _testProduct2, _testProduct3], _testProduct1.Price + _testProduct2.Price + _testProduct3.Price),
            ("INVALIDCODE", [_testProduct1, _testProduct2], _testProduct1.Price + _testProduct2.Price),
            ("INVALIDCODE", [_testProduct1, _testProduct2, _testProduct3], _testProduct1.Price + _testProduct2.Price + _testProduct3.Price),
            ("5USDOFF", [_testProduct1, _testProduct2], Math.Max(0, (_testProduct1.Price + _testProduct2.Price - 5.00m))),
            ("5USDOFF", [_testProduct1, _testProduct2, _testProduct3], Math.Max(0, (_testProduct1.Price + _testProduct2.Price + _testProduct3.Price - 5.00m))),
        ];

        private static (string, string)[] _discounts =
        [
            ("BOGOFREE", "Buy One Get One Free on any item"),
            ("BRAND2DISCOUNT", "10% off all items from Brand2"),
            ("10PERCENTOFF", "10% off the entire cart"),
            ("BRAND1MANIA", "50% off every item from Brand1"),
            ("5USDOFF", "$5 off the entire cart")
        ];

        private static void RunTest((string, Product[], decimal) scenario)
        {
            Console.WriteLine("\n\n=====================================");
            var (code, products, expectedTotal) = scenario;
            var cart = new ShoppingCart();
            foreach (var product in products)
            {
                cart.AddItem(product);
            }
            cart.ApplyDiscount(code);
            var total = cart.GetTotal();

            Console.WriteLine("Shopping Cart:");
            foreach (var item in cart.Items)
            {
                Console.WriteLine($"- {item.Brand} {item.Name}: ${item.Price}");
            }
            Console.WriteLine($"Discount Code: {code}");
            Console.WriteLine($"Total before discount: ${products.Sum(p => p.Price)}");
            Console.WriteLine($"Total after discount: ${total} (expected ${expectedTotal})");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Available Discount Codes:");
            foreach (var (code, description) in _discounts)
            {
                Console.WriteLine($"{code}: {description}");
            }

            Console.WriteLine();
            foreach (var scenario in _testCases)
            {
                RunTest(scenario);
            }
            Console.WriteLine("=====================================\n\n");
            Console.WriteLine("All tests completed.");
            Console.ReadLine();
        }


    }
}
