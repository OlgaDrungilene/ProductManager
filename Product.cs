using System;
using System.Collections.Generic;

namespace ProductManager
{
    public partial class Product
    {
        public Product()
        {
            ProductsCategories = new HashSet<ProductsCategory>();
        }

        public int Id { get; set; }
        public string ArticleNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<ProductsCategory> ProductsCategories { get; set; }
    }
}
