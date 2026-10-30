using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public abstract class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Product(int productId, string name, double price)
        {
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(
                            "Price must be greater than 0");
            }

            ProductId = productId;
            Name = name;
            Price = price;
        }

        public void ApplyDiscount(double percent)
        {
            if (percent < 0 || percent > 100)
            {
                throw new ArgumentOutOfRangeException(
                            "Discount percent must be between 0 and 100");
            }
            Price = Price * (1 - percent / 100);
        }

        public abstract string GetCategory();
    }
}