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

        public void SaveProduct(string categoryName, Product product)
        {

        }

        public List<ProductCategory> GetAllCategories()
        {
            List<ProductCategory> categories = new List<ProductCategory>();
            return categories
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
                    @Description
                    @ImageURL,
                    @Price
                )
            ";

            using SqlConnection connection = new(ConnectionString);

            using SqlCommand command = new(sql, connection);

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
            // TODO:implement SQL operator
            return true;
        }

        public Product GetProduct(string articleNumber)
        {
            //TODO:implement SQL operator
            return null;
        }
        
         public void RemoveProduct (string articleNumber)
        {
            //TODO: implement SQL Operator
            return;

        }

        public void AddCategory (ProductCategory category)
        {
            //TODO implement Sql
            return;
        }
    
        public bool IsCategoryPresent(string category)
        {
            //TODO implement Sql
            return true;
        }
    
        public void SaveProduct (string categoryName, a)
        {
            //TODO implement SQL
            return;
        }


    }
}
