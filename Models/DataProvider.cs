﻿using System;
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
            string sql = @"
              SELECT COUNT(*) FROM Users
              WHERE Name = @Name AND Password = @Password
            ";
            using SqlConnection connection = new(ConnectionString);

            using SqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("@Name", userName);
            command.Parameters.AddWithValue("@Password", password);

            connection.Open();

            var reader = command.ExecuteReader();
            reader.Read();

            if (reader.GetInt32(0) == 0)
                return false;

            return true;
        }

        public void SaveProduct(Product product)
        {
            string sql = @"
                INSERT INTO Products (
                    ArticleNumber,
                    Name, 
                    Description,
                    ImageURL,
                    Price
                ) VALUES (
                    @ArticleNumber,
                    @Name,
                    @Description,
                    @ImageURL,
                    @Price
                )
            ";

            using SqlConnection connection = new(ConnectionString);

            using SqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("@ID", product.ID);
            command.Parameters.AddWithValue("@ArticleNumber", product.ArticleNumber);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@ImageURL", product.ImageURL);
            command.Parameters.AddWithValue("@Price", product.Price);

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
        }

        public bool IsArticlePresent(string articleNumber)
        {
            string sql = @"
              SELECT COUNT(*) FROM Products
              WHERE ArticleNumber = @ArticleNumber;
            ";
            using SqlConnection connection = new(ConnectionString);

            using SqlCommand command = new(sql, connection);
            
            command.Parameters.AddWithValue("@ArticleNumber", articleNumber);

            connection.Open();

            var reader = command.ExecuteReader();
            reader.Read();

             if (reader.GetInt32(0) == 0)
             return false;

            return true;
        }

        public Product GetProduct(string articleNumber)
        {
             string sql = @"
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

            Product product = new Product();
            product.ID= reader.GetInt32(0);
            product.ArticleNumber = reader.GetString(1);
            product.Name = reader.GetString(2);
            product.Description = reader.GetString(3);
            product.ImageURL = reader.GetString(4);
            product.Price = reader.GetDecimal(5);
            
            return product;
        }

        public void RemoveProduct(string articleNumber)
        {
            string sql = @"
                    DELETE FROM Products 
                    WHERE ArticleNumber = @articleNumber
            ";

            using SqlConnection connection = new(ConnectionString);

            using SqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("@articleNumber", articleNumber);

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
            return;

        }
        public void AddCategory(Category category)
        {
            string sql = @"
                INSERT INTO Categories (
                    Name, 
                    Description,
                    ImageURL
                ) VALUES (
                    @Name,
                    @Description,
                    @ImageURL
                )
            ";

            using SqlConnection connection = new(ConnectionString);

            using SqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("@Name", category.Name);
            command.Parameters.AddWithValue("@Description", category.Description);
            command.Parameters.AddWithValue("@ImageURL", category.ImageUrl);

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
            return;
        }

        public bool IsCategoryPresent(string name)
        {
            string sql = @"
              SELECT COUNT(*) FROM Categories
              WHERE Name = @Category;
            ";
            using SqlConnection connection = new(ConnectionString);

            using SqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("@Category", name);

            connection.Open();

            var reader = command.ExecuteReader();
            reader.Read();

            if (reader.GetInt32(0) == 0)
            return false;

            return true;
        }
        public void SaveProduct(string categoryName, Product product)
        {
            string sql = @"
                  INSERT INTO ProductsCategories (IDProduct, IDCategory) 
                  (SELECT @productId, ID FROM Categories WHERE Name = @categoryName)
            ";

             using SqlConnection connection = new(ConnectionString);

             using SqlCommand command = new(sql, connection);

             command.Parameters.AddWithValue("@categoryName", categoryName);
             command.Parameters.AddWithValue("@productId", product.ID);

             connection.Open();

             command.ExecuteNonQuery();

             }

        public void PopulateCategoryProducts(Category category)
        {
            string sql = @"
                
                SELECT ID,
                       ArticleNumber,
                       Name,
                       Description,
                       ImageURL,
                       Price
                  FROM Products
                 WHERE ID IN 
				 (SELECT IDProduct FROM ProductsCategories WHERE IDCategory = @idCategory)
            ";

            using SqlConnection connection = new(ConnectionString);

            using SqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("@idCategory", category.ID);

            connection.Open();

            var reader = command.ExecuteReader();
           
            while (reader.Read())
            {
                Product p = new Product();
                p.ID = reader.GetInt32(0);
                p.Name = reader.GetString(2);
                p.Price = reader.GetDecimal(5);
                
                category.Products.Add(p);
               
            }
            return;
        }

        public List<Category> GetAllCategories()
        {
            string sql = @"
                SELECT ID,
                       Name,
                       Description,
                       ImageUrl
                 FROM  Categories
            ";

            using SqlConnection connection = new(ConnectionString);
          
            using SqlCommand command = new(sql, connection);
            
            List<Category> categories = new List<Category>();
            connection.Open();
            
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["ID"];
                var name = (string)reader["Name"];
                var description = (string)reader["Description"];
                var imageUrl = (string)reader["ImageUrl"];

                Category productCategory = new(name, description, imageUrl)
                {
                    ID = id
                };
                categories.Add(productCategory);
            }
            return categories;
        }

        internal bool IsProductInCategory(Product a, string categoryName)
        {
            string sql = @"
             SELECT COUNT(*) 
               FROM ProductsCategories 
              WHERE IDProduct = @IDProduct AND IDCategory IN (SELECT ID FROM Categories WHERE Name = @Name)

            ";
            using SqlConnection connection = new(ConnectionString);

            using SqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("@IDProduct", a.ID);
            command.Parameters.AddWithValue("@Name", categoryName);

            connection.Open();

            var reader = command.ExecuteReader();
            reader.Read();

            if (reader.GetInt32(0) == 0)
                return false;

            return true;
        }
        public List<Category> GetAllCategories(int? parentId)
        {
            string sql = "";

            using SqlConnection connection = new(ConnectionString);

            using SqlCommand command = connection.CreateCommand();

            if (parentId == null)
            {
                sql = @"
                SELECT ID,
                       Name,
                       Description,
                       ImageUrl
                 FROM  Categories
                 WHERE IDParent IS NULL
                ";                
            } 
            else
            {
                sql = @"
                SELECT ID,
                       Name,
                       Description,
                       ImageUrl
                 FROM  Categories
                 WHERE IDParent = @parentId
                ";
                command.Parameters.AddWithValue("@parentId", parentId);
            }

            command.CommandText = sql;

            List<Category> categories = new List<Category>();
            connection.Open();

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["ID"];
                var name = (string)reader["Name"];
                var description = (string)reader["Description"];
                var imageUrl = (string)reader["ImageUrl"];

                Category productCategory = new(name, description, imageUrl)
                {
                    ID = id
                };
                categories.Add(productCategory);
            }
            return categories;

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
     public int GetProductsCount(Category category)
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
