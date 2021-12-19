using System;
using System.Collections.Generic;

namespace ProductManager
{
    public partial class ProductsCategory
    {
        public int Id { get; set; }
        public int? Idproduct { get; set; }
        public int? Idcategory { get; set; }

        public virtual Category IdcategoryNavigation { get; set; }
        public virtual Product IdproductNavigation { get; set; }
    }
}
