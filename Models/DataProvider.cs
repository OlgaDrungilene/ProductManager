using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager
{
    internal class DataProvider
    {
        string ConnectionString;
        public DataProvider(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public bool IsUserPresent(string userName, string password)
        {
            using var context = new ProductManagerContext();
           
            return context.Users.Count(u => u.Name == userName && u.Password == password) > 0;
        }

        public void SaveProduct(ProductInfo product)
        {
            using var context = new ProductManagerContext();
            {
                Product p = new Product();
                p.ArticleNumber = product.ArticleNumber;
                p.Name = product.Name;
                p.Description = product.Description;
                p.ImageUrl = product.ImageURL;
                p.Price = product.Price;

                context.Products.Add(p);
                context.SaveChanges();

            }
            
        }

        public bool IsArticlePresent(string articleNumber)
        {
            using var context = new ProductManagerContext();
            return context.Products.Count(p => p.ArticleNumber==articleNumber) > 0;
        }

        public ProductInfo GetProduct(string articleNumber)
        {
            using var context = new ProductManagerContext();
            return context.Products.Where(p => p.ArticleNumber == articleNumber).Select(p => new ProductInfo
            {
                ID = p.Id,
                Name = p.Name,
                Description = p.Description,
                ArticleNumber = p.ArticleNumber,
                ImageURL = p.ImageUrl,
                Price = p.Price,
            }).FirstOrDefault();
            
        }

        public void RemoveProduct(string articleNumber)
        {
            // TODO: implement 
            using var context = new ProductManagerContext();
            Product product = context.Products.FirstOrDefault(p => p.ArticleNumber == articleNumber);
            context.Products.Remove(product);
            
        }
        public void AddCategory(CategoryInfo category)
        {
            using var context = new ProductManagerContext();
            {
                Category c = new Category();
                c.Name = category.Name;
                c.Description = category.Description;
                c.ImageUrl = category.ImageUrl;

                context.Categories.Add(c);
                context.SaveChanges();

            }
        }

        public bool IsCategoryPresent(string name)
        {
            using var context = new ProductManagerContext();
            {
                return context.Categories.Count(c => c.Name == name) > 0;
            }
           
        }
        public void SaveProduct(string categoryName, ProductInfo product)

        {
            using var context = new ProductManagerContext();
            {
                ProductsCategory p = new ProductsCategory();
                p.IdProduct = product.ID;
                Category c = context.Categories.FirstOrDefault(c => c.Name == categoryName);
                p.IdCategory = c.Id;
                
                context.ProductsCategories.Add(p);
                context.SaveChanges();

            }
            
        }

        public void PopulateCategoryProducts(CategoryInfo category)
        {
            using var context = new ProductManagerContext();
            category.Products = context.Categories.
                Where(c => c.Id == category.ID).
                SelectMany(c => c.ProductsCategories).
                Select(p => p.IdProductNavigation).
                Select(p => new ProductInfo {
                ArticleNumber = p.ArticleNumber,
                Name = p.Name,
                Description = p.Description,
                ImageURL=p.ImageUrl,
                Price = p.Price,
                }).ToList();
          
        }

        public List<CategoryInfo> GetAllCategories()
        {
            using var context = new ProductManagerContext();
            {
                return context.Categories.Select(c => new CategoryInfo(c.Name, c.Description,c.ImageUrl)
                ).ToList();
            }
           
        }

        internal bool IsProductInCategory(ProductInfo a, string categoryName)
        {
            using var context = new ProductManagerContext();
            int count = context.ProductsCategories.Count(x => x.IdProductNavigation.Id == a.ID && x.IdCategoryNavigation.Name == categoryName);

            return count > 0;
            
        }
        public List<CategoryInfo> GetAllCategories(int? parentId)
        {
            using var context = new ProductManagerContext();
            {
                return context.Categories.Where (c=>c.Idparent==parentId).Select(c => new CategoryInfo(c.Name, c.Description, c.ImageUrl)
                {
                    ID = c.Id
                }
                ).ToList();
            }            
        }
        public void AddCategoryToCategory(string parentCategory,string childCategory)
       
        {
            using var context = new ProductManagerContext();

            var pcat = context.Categories.FirstOrDefault(c => c.Name == parentCategory);

            if (pcat == null)
                return;

            var ccat = context.Categories.FirstOrDefault(x => x.Name == childCategory);

            if (ccat == null)
                return;

            ccat.Idparent = pcat.Id;
            context.SaveChanges();

        }

        public int GetProductsCount(CategoryInfo category)
        {
            using var context = new ProductManagerContext();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = @"WITH cte AS(
               SELECT ID FROM Categories WHERE IDParent = @ID UNION ALL
               SELECT c.ID FROM Categories c JOIN cte ON c.IDParent = cte.ID
                                                )
               SELECT COUNT(*) FROM ProductsCategories
                               WHERE IDCategory IN(SELECT * FROM cte UNION ALL SELECT @ID)
                ";
            var p = command.CreateParameter();
            p.ParameterName = "@ID";
            p.Value = category.ID;
            command.Parameters.Add(p);

            context.Database.OpenConnection();

            var reader = command.ExecuteReader();
            reader.Read();

            return (reader.GetInt32(0));

        }

    }
}
