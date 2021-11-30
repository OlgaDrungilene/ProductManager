using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager
{
    public class ProductCategory
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public string ImageUrl { get; private set; }

        public List<Product> Products;
        public void AddProduct(Product product) { Products.Add(product);}
          

        public ProductCategory(string name, string description, string imageURL)
        {
            Name = name;
            Description = description;
            ImageUrl = imageURL;
            Products = new List<Product>();
           
        }
       
    }
}
