using System;
using System.Collections.Generic;

namespace ProductManager
{
    public partial class Category
    {
        public Category()
        {
            InverseIdparentNavigation = new HashSet<Category>();
            ProductsCategories = new HashSet<ProductsCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int? Idparent { get; set; }

        public virtual Category IdparentNavigation { get; set; }
        public virtual ICollection<Category> InverseIdparentNavigation { get; set; }
        public virtual ICollection<ProductsCategory> ProductsCategories { get; set; }
    }
}
