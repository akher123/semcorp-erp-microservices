namespace Ordering.Infrastructure.Extensions;

public class InitialData
{
    // =========================
    // 🇧🇩 Seed: Customers
    // =========================
    public static IEnumerable<Customer> Customers =>
        new List<Customer>
        {
            Customer.Create(
                CustomerId.Of(new Guid("b0cd2ec7-40f1-4be7-8ec7-bdc193fc20c1")),
                "rahimuddin",
                "rahim.uddin@outlook.com"),

            Customer.Create(
                CustomerId.Of(new Guid("f58c9fe4-30f9-4ec2-8b1b-7ff2fc3ad475")),
                "fatimaakter",
                "fatima.akter@gmail.com")
        };


    // =========================
    // 🇧🇩 Seed: Products
    // =========================
    public static IEnumerable<Product> Products =>
        new List<Product>
        {
            Product.Create(
                ProductId.Of(new Guid("0c1b0f4e-dc7d-4e3b-bb1f-02870aa009c5")),
                "Walton Primo N5",
                18000),

            Product.Create(
                ProductId.Of(new Guid("29c446d3-2f70-4cb0-9efb-99ece15cec06")),
                "Xiaomi Redmi Note 12",
                22000),

            Product.Create(
                ProductId.Of(new Guid("594cbd53-a046-4c3e-a321-7caa62f340bc")),
                "Samsung A54",
                35000),

            Product.Create(
                ProductId.Of(new Guid("cc42e279-4335-4f0d-8a65-a450fe5ee40d")),
                "Symphony Z60",
                12500)
        };



    // =========================
    // 🇧🇩 Seed: Orders with Items
    // =========================
    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            
            // =========================
            // Order 1 (Rahim)
            // =========================
            var order1 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("b0cd2ec7-40f1-4be7-8ec7-bdc193fc20c1")),
                OrderName.Of("ORD_001"),
                emailAddress: EmailAddress.Of("rahim@gmail.com"));

            order1.Add(ProductId.Of(new Guid("0c1b0f4e-dc7d-4e3b-bb1f-02870aa009c5")), 1, 18000);  // Walton Primo
            order1.Add(ProductId.Of(new Guid("29c446d3-2f70-4cb0-9efb-99ece15cec06")), 2, 22000);  // Xiaomi Note 12


            // =========================
            // Order 2 (Fatima)
            // =========================
            var order2 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("f58c9fe4-30f9-4ec2-8b1b-7ff2fc3ad475")),
                OrderName.Of("ORD_002"),
                emailAddress: EmailAddress.Of("fatima@gmail.com"));

            order2.Add(ProductId.Of(new Guid("594cbd53-a046-4c3e-a321-7caa62f340bc")), 1, 35000);  // Samsung A54
            order2.Add(ProductId.Of(new Guid("cc42e279-4335-4f0d-8a65-a450fe5ee40d")), 1, 12500);  // Symphony Z60


            return new List<Order> { order1, order2 };
        }
    }
}

