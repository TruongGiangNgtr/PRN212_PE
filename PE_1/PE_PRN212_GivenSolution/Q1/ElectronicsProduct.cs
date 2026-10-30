using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class ElectronicsProduct : Product, IDiscountable
    {
        public string Brand { get; set; }
        public int WarrantyMonths { get; set; }

        public ElectronicsProduct(int productId, string name, double price, string brand, int warrantyMonths)
            : base(productId, name, price)
        {
            WarrantyMonths = warrantyMonths;
            Brand = brand;
        }

        public override string GetCategory()
        {
            return "Electronics";
        }
    }
}