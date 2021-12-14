using ProductManager.Models;
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
        
        static void Authenticate(DataProvider dataProvider)
        {
           
            bool invalidUser;

            invalidUser = false;

            do
            {
                Clear();
                Console.WriteLine("Username:\nPassword:");

                SetCursorPosition(10, 0);
                string userName = Console.ReadLine();

                SetCursorPosition(10, 1);
                string password = Console.ReadLine();

                invalidUser = !dataProvider.IsUserPresent(userName, password);
               
                if (invalidUser)
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

                            AddCategory(out name, out description, out imageURL);

                            Write("\nIs this correct Y(es) N(o)");

                        } while (ReadKey(true).Key == ConsoleKey.N);

                        Category category = new Category(name, description, imageURL);

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
                                if (dataProvider.IsProductInCategory(a, categoryName))
                                {
                                    WriteLine("This product already exists");
                                }
                                else
                                {
                                    dataProvider.SaveProduct(categoryName, a);

                                    WriteLine("Product added to category");
                                }
                            }
                            else
                            {
                                WriteLine("Category not found");
                            }

                            Thread.Sleep(2000);

                            }
                        else
                        {
                            WriteLine("Product not found");

                            Thread.Sleep(2000);
                        }
                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:

                        WriteLine("Name                                         Price");
                        WriteLine("---------------------------------------------------------");

                        PrintCategory(dataProvider, null, 0);
                       
                        WaitForEscape();

                        break;

                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:

                        while (true)
                        {

                            Clear();

                            WriteLine("Parent category: \n Child category:");

                            SetCursorPosition(17, 0);
                            string parentCategory = ReadLine();

                            if (!dataProvider.IsCategoryPresent(parentCategory))
                            {
                                WriteLine("Category not found");

                                Thread.Sleep(2000);
                                break;
                            }

                            SetCursorPosition(16, 1);
                            string childCategory = ReadLine();

                            if (!dataProvider.IsCategoryPresent(childCategory))
                            {
                                WriteLine("Category not found");

                                Thread.Sleep(2000);
                                break;
                            }

                            WriteLine("Is this correct? (Y)es (N)o");

                            ConsoleKey key;
                            do
                            {
                                key = ReadKey(true).Key;
                            } while (key != ConsoleKey.Y && key != ConsoleKey.N);

                            if (key == (ConsoleKey.Y))
                            {
                                dataProvider.AddCategoryToCategory(parentCategory,childCategory);

                                WriteLine("Categories connected");

                                Thread.Sleep(2000);

                                break;
                            }

                        }
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:

                        return;

                }
            }

        }

        private static void PrintCategory(DataProvider dataProvider, int? parentId, int level)
        {
            // 1. Få alla PARENTID = parentId 

            List <Category> categories = dataProvider.GetAllCategories(parentId);

            // 2. För varje kategori, som vi får

            foreach (Category category in categories)
            {
                dataProvider.PopulateCategoryProducts(category);

                
                if (level == 0)
                {
                    ForegroundColor = ConsoleColor.Green;
                    int productsCount = dataProvider.GetProductsCount(category);

                    WriteLine(new String(' ', level * 2) + category.Name + " (" + productsCount + ")");
                }

                else
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(new String(' ', level * 2) + category.Name);
                }
                    ForegroundColor = ConsoleColor.White;
                foreach (Product product in category.Products)
                {
                    WriteLine((new String(' ', (level + 1) * 2) + product.Name).PadRight(45) + product.Price);
                }
                
                PrintCategory(dataProvider, category.ID, level + 1);
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


        static void Main(string[] args)
        {
            DataProvider dataProvider = new DataProvider(connectionString);
            while (true)
            {
                Authenticate(dataProvider);

                DoMainMenu(dataProvider);
            }
        }
    }
            
}
        
    

