﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; private set; }

        public string Description { get; private set; }

        public string ImageUrl { get; private set; }

        public List<Product> Products;
       

        public Category( string name, string description, string imageUrl)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Products = new List<Product>();
           
        }

    }
}
