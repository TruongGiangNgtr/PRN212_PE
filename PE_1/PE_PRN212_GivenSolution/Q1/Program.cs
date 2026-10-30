using Q1;

public delegate void DiscountAppliedCallback(string productName, double discountPercent);

public class Program
{
    public static void Main(string[] args)
    {
        ElectronicsProduct product1 = new ElectronicsProduct(1, "Laptop", 1000.0, "Lenovo", 24);
        ElectronicsProduct product2 = new ElectronicsProduct(2, "Smartphone", 500.5, "Apple", 12);
        ElectronicsProduct product3 = new ElectronicsProduct(3, "Tablet", 300.0, "Samsung", 18);

        DiscountAppliedCallback callback = (productName, discountPercent) =>
        {
            Console.WriteLine($"[DISCOUNT]: {productName} received {discountPercent}% discount");
        };

        DiscountManager discountManager = new DiscountManager(callback);

        discountManager.AddProduct(product1);
        discountManager.AddProduct(product2);
        discountManager.AddProduct(product3);

        try
        {
            discountManager.ApplyDiscountToProduct(1, 10); // Apply 10% discount to Laptop
            discountManager.ApplyDiscountToProduct(2, 20); // Apply 20% discount to Smartphone
        }
        catch (Exception ex)
        {
            throw new ArgumentOutOfRangeException();
        }

        discountManager.GetProductsUnder(500.0);
    }
}