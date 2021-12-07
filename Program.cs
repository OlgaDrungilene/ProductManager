//using ProductManager.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;
using System.Threading;
using System.Data.SqlClient;

namespace ProductManager
{
    class Program
    {
        static string connectionString = "Server=.;Database=ProductManager;Integrated Security=True";
        
        static void Authenticate(string userName, string password, DataProvider dataProvider)
        {
           
            bool invalidUser;

            invalidUser = false;

            do
            {
                Clear();
                Console.WriteLine("Username:\nPassword:");

                SetCursorPosition(10, 0);
                userName = Console.ReadLine();

                SetCursorPosition(10, 1);
                password = Console.ReadLine();

               
                if (dataProvider.IsUserPresent(userName, password ))
                {
                    Write("Invalid credentials, please try again");

                    Thread.Sleep(2000);
                }

            } while (invalidUser);
        }
        static void WaitForEscape()
        {
            while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;
        }
        static void DoMainMenu(DataProvider dataProvider)
        {
            while (true)
            {
                Clear();

                WriteLine("1. Add product");
                WriteLine("2. Search product");
                WriteLine("3. Add category");
                WriteLine("4. Add product to category");
                WriteLine("5. List categories");
                WriteLine("6. Add category to category");
                WriteLine("7. Logout");

                ConsoleKeyInfo input;

                bool invalidChoice;

                do
                {
                    input = ReadKey(true);

                    invalidChoice = !(input.Key == ConsoleKey.D1 || input.Key == ConsoleKey.NumPad1
                                   || input.Key == ConsoleKey.D2 || input.Key == ConsoleKey.NumPad2
                                   || input.Key == ConsoleKey.D3 || input.Key == ConsoleKey.NumPad3
                                   || input.Key == ConsoleKey.D4 || input.Key == ConsoleKey.NumPad4
                                   || input.Key == ConsoleKey.D5 || input.Key == ConsoleKey.NumPad5
                                   || input.Key == ConsoleKey.D6 || input.Key == ConsoleKey.NumPad6
                                   || input.Key == ConsoleKey.D7 || input.Key == ConsoleKey.NumPad7);
                }
                while (invalidChoice);

                Clear();

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:

                        Product p = new();
                        do
                        {
                            Clear();

                            AddProduct(p);

                            Write("Is this correct Y(es) N(o)");

                        } while (ReadKey(true).Key == ConsoleKey.N);

                        //if (products.ContainsKey(p.ArticleNumber))
                        if (dataProvider.IsArticlePresent(p.ArticleNumber))
                        {
                            WriteLine("Product already exists");
                        }
                        else
                        {
                            dataProvider.SaveProduct(p);
                            //products.Add(p.ArticleNumber, p);

                            WriteLine("Product saved");
                        }

                        Thread.Sleep(2000);

                        Clear();

                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:

                        Write("Article number:");
                        string articleNumber = ReadLine();

                        //if (products.ContainsKey(articleNumber))
                        if (dataProvider.IsArticlePresent(articleNumber))
                        {
                            Product a = dataProvider.GetProduct(articleNumber);
                            
                            ConsoleKey key;
                           
                            while (true)
                            {

                                Clear();

                                Print(a);

                                WriteLine("\n(D)elete");
                                WriteLine("\nPress Escape (Esc) key to quit: ");

                                do
                                {
                                    key = ReadKey(true).Key;
                               
                                } while (key != ConsoleKey.Escape && key != ConsoleKey.D);

                                if (key == (ConsoleKey.D))
                                {
                                    Clear();

                                    Print(a);

                                    WriteLine("Are you sure you want to delete? (Y)es (N)o");
                                    do
                                    {
                                        key = ReadKey(true).Key;
                                    } while (key != ConsoleKey.Y && key != ConsoleKey.N);

                                    if (key == (ConsoleKey.Y))
                                    {
                                        dataProvider.RemoveProduct(articleNumber);

                                        WriteLine("Product deleted");
                                       
                                        Thread.Sleep(2000);

                                        break;
                                    }
                                    Clear();
                                }
                                else
                                {
                                    Clear();

                                    break;
                                }
                            }
                        }
                        else
                        {
                            WriteLine("Product not found");

                            Thread.Sleep(2000);
                        }

                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:

                        string name, description, imageURL;
                        
                        do
                        {
                            Clear();

                            AddCategory( out name, out description, out imageURL);

                            Write("\nIs this correct Y(es) N(o)");

                        } while (ReadKey(true).Key == ConsoleKey.N);

                        ProductCategory category = new ProductCategory( name, description, imageURL);

                        if (dataProvider.IsCategoryPresent(category.Name))
                        {
                            Write("This category already exists");
                        }
                        else
                        {
                            dataProvider.AddCategory(category);

                            WriteLine("Category added");
                        }
                      
                        Thread.Sleep(2000);
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:

                        Clear();

                        Write("Article number: ");

                        string productArticleNumber = ReadLine();

                        if (dataProvider.IsArticlePresent(productArticleNumber))

                        {
                            Product a = dataProvider.GetProduct(productArticleNumber);

                            Write("Category name: ");

                            string categoryName = ReadLine();

                            if (dataProvider.IsCategoryPresent(categoryName))
                            {
                                dataProvider.SaveProduct(categoryName,a);

                                WriteLine("Product added to category");
                            }
                            else
                            {
                                WriteLine("Category not found");
                            }

                            Thread.Sleep(2000);

                            WaitForEscape();
                        }
                        else
                        {
                            WriteLine("Product not found");

                            Thread.Sleep(2000);
                        }
                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:

                        WriteLine("Name                         Price");
                        WriteLine("-------------------------------------------------");

                        foreach (ProductCategory cat in dataProvider.GetAllCategories())
                        {
                            WriteLine(cat.Name + " (" + cat.Products.Count + ")");

                            foreach (Product product in cat.Products)
                            {
                                WriteLine("  " + product.Name + "\t\t" + product.Price);
                            }
                          
                        }
                        WaitForEscape();

                        break;

                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:

                        Clear();

                       // TODO
                       // string parentCategory = ReadLine();
                       // string childCategory = ReadLine();

                        WriteLine ("Parent category: ");
                        WriteLine ("Child category: ");

                        break;

                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:

                        return;

                }
            }

        }

        /// <summary>
        /// Method populates temporary data
        /// </summary>
        /// <param name="products"></param>
        /// <param name="productCategory"></param>
        //static void PopulateData(Dictionary<string, Product> products, Dictionary<string, ProductCategory> productCategory)
        //{
        //    Product temporaryProduct = new()
        //    {
        //        ArticleNumber = "123",
        //        Name = "T-Shirts",
        //        Price = 200
        //    };
        //    products.Add(temporaryProduct.ArticleNumber, temporaryProduct);

        //    ProductCategory temporaryProductCategory = new("Clothes", "...", "...");
        //    temporaryProductCategory.Products.Add(temporaryProduct);
        //    productCategory.Add(temporaryProductCategory.Name, temporaryProductCategory);
        //}
        static void Print(Product p)
        {
            WriteLine($"ID:{p.ID}");
            WriteLine($"Article number:{p.ArticleNumber}");
            WriteLine($"Name: {p.Name}");
            WriteLine($"Description: {p.Description}");
            WriteLine($"Image URL: {p.ImageURL}");
            WriteLine($"Price: {p.Price}");
        }
        private static void AddCategory( out string categoryname, out string description, out string imageURL)
        {
            
            WriteLine("Name:");
            WriteLine("Description:");
            WriteLine("Image URL:");

            //SetCursorPosition(4, 0);
            //id = ReadLine();//string?Sql INT.

            SetCursorPosition(6, 0);
            categoryname = ReadLine();

            SetCursorPosition(12, 1);
            description = ReadLine();

            SetCursorPosition(10, 2);
            imageURL = ReadLine();
        }

        private static void AddProduct(Product p)
        {
            WriteLine("Article number:");
            WriteLine("Name:");
            WriteLine("Description:");
            WriteLine("Image URL:");
            WriteLine("Price:");

            SetCursorPosition(16, 0);
            p.ArticleNumber = ReadLine();

            SetCursorPosition(6, 1);
            p.Name = ReadLine();

            SetCursorPosition(13, 2);
            p.Description = ReadLine();

            SetCursorPosition(11, 3);
            p.ImageURL = ReadLine();

            SetCursorPosition(7, 4);
            p.Price = Convert.ToDecimal(ReadLine());
        }

     
        //static void Main(string[] args)
        //{
           
        //    Dictionary<string, string> userLogin = new();
        //    userLogin.Add("Tina", "strategi");
        //    userLogin.Add("Alex", "skydd");

        //    DataProvider dataProvider = new DataProvider(connectionString);

        //    while (true)
        //    {
        //        Authenticate(userLogin);

        //        DoMainMenu(dataProvider);
        //    }
           

        //}
    }
        
    
}
        
    

