using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wholesaler
{
    public class ProductPrize
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }

        public virtual ICollection<Product>
            Products
        { get; set; } =
            new ObservableCollection<Product>();
    }
}
