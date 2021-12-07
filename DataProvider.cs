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
                INSERT INTO Products (
                    ArticleNumber,
                   
                ) VALUES (
                    @ArticleNumber,
                )
            ";

            using SqlConnection connection = new(ConnectionString);

            using SqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("@ArticleNumber", articleNumber);

            connection.Open();
            var reader = command.ExecuteReader();
           
            if  (reader.Read()==false)
            
            return null;

            Product product = new Product();
            product.ID = reader.GetInt32(0);
            product.ArticleNumber = reader.GetString(1);
            product.Name = reader.GetString(2);
            product.ImageURL = reader.GetString(3);
            product.Description = reader.GetString(4);
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

        public void AddCategory(ProductCategory category)
        {
           /* string sql = @"
                INSERT INTO ProductCategories (
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
            return;*/
        }

        public bool IsCategoryPresent(string name)
        {
            //TODO implement Sql
            string sql = @"
              SELECT COUNT(*) FROM ProductCategories
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
                  UPDATE Products SET IDCategory = 
     (SELECT ID FROM ProductCategories WHERE Name = @categoryName)
     WHERE ID = @productId
             ";

             using SqlConnection connection = new(ConnectionString);

             using SqlCommand command = new(sql, connection);

             command.Parameters.AddWithValue("@categoryName", categoryName);
            command.Parameters.AddWithValue("@productId", product.ID);

             connection.Open();

             command.ExecuteNonQuery();

             }

        public List<ProductCategory> GetAllCategories()
        {
            throw new NotImplementedException();
           /* List<ProductCategory> categories = new List<ProductCategory>();

            //string sql = @"
            //    SELECT ID,
            //           Name,
            //           Description,
            //           ImageURL
            //    FROM ProductCategories
            //";
            //using SqlConnection connection = new(ConnectionString);
            //using SqlCommand command = new (sql, connection);

            //connection.Open();

            //var reader = command.ExecuteReader();

                        //while (reader.Read())
            //{
            //    var productCategory = new ProductCategory(id: (int) reader ["ID"],
            //                                              name: (string)reader["Name"],
            //                                              description: (string)reader["Description"],
            //                                              imageURL: (string)reader["ImageURL"]);
                
            //    productCategoryList.Add(productCategory);                                             
            //}
            return categories;*/
        }

    }
}
