using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public interface IDiscountable
    {
        double Price { get; set; }

        void ApplyDiscount(double percent);
    }
}