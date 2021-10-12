using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager
{
    class ProductCategory
    {
        public string Name;

        public string Description;

        public string ImageURL;

        public List<Product> Products;
        public void AddProduct(Product product) { Products.Add(product);}
          

        public ProductCategory(string name, string description, string imageURL)
        {
            Name = name;
            Description = description;
            ImageURL = imageURL;
            Products = new List<Product>();
        }

    }
}
