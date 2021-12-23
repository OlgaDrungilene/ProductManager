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
            //string sql = @"
            //  SELECT COUNT(*) FROM Users
            //  WHERE Name = @Name AND Password = @Password
            //";
            //using SqlConnection connection = new(ConnectionString);

            //using SqlCommand command = new(sql, connection);

            //command.Parameters.AddWithValue("@Name", userName);
            //command.Parameters.AddWithValue("@Password", password);

            //connection.Open();

            //var reader = command.ExecuteReader();
            //reader.Read();

            //if (reader.GetInt32(0) == 0)
            //    return false;

            //return true;
            using var context = new ProductManagerContext();
            /*int usersCount = context.Users.Count(u => u.Name == userName && u.Password == password);
            if (usersCount > 0)
            {
                return true;
            }
            return false;*/
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
            // TODO: implement 
            //string sql = @"
            //    INSERT INTO Products (
            //        ArticleNumber,
            //        Name, 
            //        Description,
            //        ImageURL,
            //        Price
            //    ) VALUES (
            //        @ArticleNumber,
            //        @Name,
            //        @Description,
            //        @ImageURL,
            //        @Price
            //    )
            //";

            //using SqlConnection connection = new(ConnectionString);

            //using SqlCommand command = new(sql, connection);

            //command.Parameters.AddWithValue("@ArticleNumber", product.ArticleNumber);
            //command.Parameters.AddWithValue("@Name", product.Name);
            //command.Parameters.AddWithValue("@Description", product.Description);
            //command.Parameters.AddWithValue("@ImageURL", product.ImageURL);
            //command.Parameters.AddWithValue("@Price", product.Price);

            //connection.Open();

            //command.ExecuteNonQuery();

            //connection.Close();
        }

        public bool IsArticlePresent(string articleNumber)
        {
            //string sql = @"
            //  SELECT COUNT(*) FROM Products
            //  WHERE ArticleNumber = @ArticleNumber;
            //";
            //using SqlConnection connection = new(ConnectionString);

            //using SqlCommand command = new(sql, connection);
            
            //command.Parameters.AddWithValue("@ArticleNumber", articleNumber);

            //connection.Open();

            //var reader = command.ExecuteReader();
            //reader.Read();

            // if (reader.GetInt32(0) == 0)
            // return false;

            //return true;
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
            /* string sql = @"
                SELECT ID,
                       ArticleNumber,
                       Name,
                       Description,
                       ImageUrl,
                       Price
                  FROM Products
                 WHERE ArticleNumber = @ArticleNumber
                
             ";

            using SqlConnection connection = new(ConnectionString);

            using SqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("@ArticleNumber", articleNumber);

            connection.Open();

            var reader = command.ExecuteReader();
           
            if  (reader.Read()==false)
            
            return null;

            ProductInfo product = new ();
            product.ID= reader.GetInt32(0);
            product.ArticleNumber = reader.GetString(1);
            product.Name = reader.GetString(2);
            product.Description = reader.GetString(3);
            product.ImageURL = reader.GetString(4);
            product.Price = reader.GetDecimal(5);
            
            return product;*/

        }

        public void RemoveProduct(string articleNumber)
        {
            // TODO: implement 
            using var context = new ProductManagerContext();
            Product product = context.Products.FirstOrDefault(p => p.ArticleNumber == articleNumber);
            context.Products.Remove(product);
                        
            //string sql = @"
            //        DELETE FROM Products 
            //        WHERE ArticleNumber = @articleNumber
            //";

            //using SqlConnection connection = new(ConnectionString);

            //using SqlCommand command = new(sql, connection);

            //command.Parameters.AddWithValue("@articleNumber", articleNumber);

            //connection.Open();

            //command.ExecuteNonQuery();

            //connection.Close();
            //return;

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
            // TODO: implement
            //string sql = @"
            //    INSERT INTO Categories (
            //        Name, 
            //        Description,
            //        ImageURL
            //    ) VALUES (
            //        @Name,
            //        @Description,
            //        @ImageURL
            //    )
            //";

            //using SqlConnection connection = new(ConnectionString);

            //using SqlCommand command = new(sql, connection);

            //command.Parameters.AddWithValue("@Name", category.Name);
            //command.Parameters.AddWithValue("@Description", category.Description);
            //command.Parameters.AddWithValue("@ImageURL", category.ImageUrl);

            //connection.Open();

            //command.ExecuteNonQuery();

            //connection.Close();
            //return;
        }

        public bool IsCategoryPresent(string name)
        {
            using var context = new ProductManagerContext();
            {
                return context.Categories.Count(c => c.Name == name) > 0;
            }
            // TODO: implement
            //string sql = @"
            //  SELECT COUNT(*) FROM Categories
            //  WHERE Name = @Category;
            //";
            //using SqlConnection connection = new(ConnectionString);

            //using SqlCommand command = new(sql, connection);

            //command.Parameters.AddWithValue("@Category", name);

            //connection.Open();

            //var reader = command.ExecuteReader();
            //reader.Read();

            //if (reader.GetInt32(0) == 0)
            //return false;

            //return true;
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
            //string sql = @"
            //      INSERT INTO ProductsCategories (IDProduct, IDCategory) 
            //      (SELECT @productId, ID FROM Categories WHERE Name = @categoryName)
            //";

            // using SqlConnection connection = new(ConnectionString);

            // using SqlCommand command = new(sql, connection);

            // command.Parameters.AddWithValue("@categoryName", categoryName);
            // command.Parameters.AddWithValue("@productId", product.ID);

            // connection.Open();

            // command.ExecuteNonQuery();

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
               
            //       string sql = @"
                
     //           SELECT ID,
     //                  ArticleNumber,
     //                  Name,
     //                  Description,
     //                  ImageURL,
     //                  Price
     //             FROM Products
     //            WHERE ID IN 
				 //(SELECT IDProduct FROM ProductsCategories WHERE IDCategory = @idCategory)
     //       ";

     //       using SqlConnection connection = new(ConnectionString);

     //       using SqlCommand command = new(sql, connection);

     //       command.Parameters.AddWithValue("@idCategory", category.ID);

     //       connection.Open();

     //       var reader = command.ExecuteReader();
           
     //       while (reader.Read())
     //       {
     //           ProductInfo p = new ProductInfo();
     //           p.ID = reader.GetInt32(0);
     //           p.Name = reader.GetString(2);
     //           p.Price = reader.GetDecimal(5);
                
     //           category.Products.Add(p);
               
     //       }
        }

        public List<CategoryInfo> GetAllCategories()
        {
            using var context = new ProductManagerContext();
            {
                return context.Categories.Select(c => new CategoryInfo(c.Name, c.Description,c.ImageUrl)
                ).ToList();
            }
            // TODO: implement
            // return context.Categories.Select(c => new CategoryInfo {`... }).ToList();
            //string sql = @"
            //    SELECT ID,
            //           Name,
            //           Description,
            //           ImageUrl
            //     FROM  Categories
            //";

            //using SqlConnection connection = new(ConnectionString);
          
            //using SqlCommand command = new(sql, connection);
            
            //List<CategoryInfo> categories = new List<CategoryInfo>();
            //connection.Open();
            
            //var reader = command.ExecuteReader();

            //while (reader.Read())
            //{
            //    var id = (int)reader["ID"];
            //    var name = (string)reader["Name"];
            //    var description = (string)reader["Description"];
            //    var imageUrl = (string)reader["ImageUrl"];

            //    CategoryInfo productCategory = new(name, description, imageUrl)
            //    {
            //        ID = id
            //    };
            //    categories.Add(productCategory);
            //}
            //return categories;
        }

        internal bool IsProductInCategory(ProductInfo a, string categoryName)
        {
            using var context = new ProductManagerContext();
            int count = context.ProductsCategories.Count(x => x.IdProductNavigation.Id == a.ID && x.IdCategoryNavigation.Name == categoryName);

            return count > 0;
            

                        //string sql = @"
            // SELECT COUNT(*) 
            //   FROM ProductsCategories 
            //  WHERE IDProduct = @IDProduct AND IDCategory IN (SELECT ID FROM Categories WHERE Name = @Name)

            //";
            //using SqlConnection connection = new(ConnectionString);

            //using SqlCommand command = new(sql, connection);

            //command.Parameters.AddWithValue("@IDProduct", a.ID);
            //command.Parameters.AddWithValue("@Name", categoryName);

            //connection.Open();

            //var reader = command.ExecuteReader();
            //reader.Read();

            //if (reader.GetInt32(0) == 0)
            //    return false;

            //return true;
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
            }            //// TODO: implement
            //string sql = "";

            //using SqlConnection connection = new(ConnectionString);

            //using SqlCommand command = connection.CreateCommand();

            //if (parentId == null)
            //{
            //    sql = @"
            //    SELECT ID,
            //           Name,
            //           Description,
            //           ImageUrl
            //     FROM  Categories
            //     WHERE IDParent IS NULL
            //    ";                
            //} 
            //else
            //{
            //    sql = @"
            //    SELECT ID,
            //           Name,
            //           Description,
            //           ImageUrl
            //     FROM  Categories
            //     WHERE IDParent = @parentId
            //    ";
            //    command.Parameters.AddWithValue("@parentId", parentId);
            //}

            //command.CommandText = sql;

            //List<CategoryInfo> categories = new List<CategoryInfo>();
            //connection.Open();

            //var reader = command.ExecuteReader();

            //while (reader.Read())
            //{
            //    var id = (int)reader["ID"];
            //    var name = (string)reader["Name"];
            //    var description = (string)reader["Description"];
            //    var imageUrl = (string)reader["ImageUrl"];

            //    CategoryInfo productCategory = new(name, description, imageUrl)
            //    {
            //        ID = id
            //    };
            //    categories.Add(productCategory);
            //}
            //return categories;

        }
       public void AddCategoryToCategory(string parentCategory,string childCategory)
       
        {  
            string sql = @"
                 UPDATE Categories SET IDParent = (SELECT ID FROM Categories WHERE Name = @ParentCategory)
                  WHERE Name = @ChildCategory
            ";

            using SqlConnection connection = new (ConnectionString);

            using SqlCommand command = new (sql, connection);

            command.Parameters.AddWithValue("@ParentCategory", parentCategory);
            command.Parameters.AddWithValue("@ChildCategory", childCategory);
            
            connection.Open();

            command.ExecuteNonQuery();

           }
     public int GetProductsCount(CategoryInfo category)
        {
            string sql = @"
              WITH cte AS(
              SELECT ID FROM Categories WHERE IDParent = @ID UNION ALL
            SELECT c.ID FROM Categories c JOIN cte ON c.IDParent = cte.ID
                          )
        SELECT COUNT(*) FROM ProductsCategories 
                       WHERE IDCategory IN(SELECT * FROM cte UNION ALL SELECT @ID)
            ";
            using SqlConnection connection = new(ConnectionString);

            using SqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("@ID", category.ID);
            
            connection.Open();

            var reader = command.ExecuteReader();
            reader.Read();

            return (reader.GetInt32(0));
           
        }

    }
}
