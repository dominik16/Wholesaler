using System;
using System.Collections.Generic;
using System.Text;

namespace Wholesaler
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }

        public int ProductPrizesId { get; set; }
        public virtual ProductPrize productprize { get; set; }
    }
}
