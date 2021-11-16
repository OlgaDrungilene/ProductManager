using System;
using System.Collections.Generic;
using static System.Console;
using System.Threading;

namespace ProductManager
{
    class Program
    {
            static void Authenticate(Dictionary<string, string> userLogins)
        {
            string username, password;

            bool invalidUser;

            do
            {
                Clear();
                Console.WriteLine("Username:\nPassword:");

                SetCursorPosition(10, 0);
                username = Console.ReadLine();

                SetCursorPosition(10, 1);
                password = Console.ReadLine();

                invalidUser = (!userLogins.ContainsKey(username) || userLogins[username] != password);

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
        static void DoMainMenu(Dictionary<string, Product> products, Dictionary<string, ProductCategory> productCategory)
        {
            while (true)
            {
                Clear();

                WriteLine("1. Add product");
                WriteLine("2. Search product");
                WriteLine("3. Add category");
                WriteLine("4. Add product to category");
                WriteLine("5. List categories");
                WriteLine("6. Logout");

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
                                   || input.Key == ConsoleKey.D6 || input.Key == ConsoleKey.NumPad6);
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

                        if (products.ContainsKey(p.ArticleNumber))
                        {
                            WriteLine("Product already exists");
                        }
                        else
                        {
                            products.Add(p.ArticleNumber, p);

                            WriteLine("Product saved");
                        }

                        Thread.Sleep(2000);

                        Clear();

                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:

                        Write("Article number:");
                        string articleNumber = ReadLine();

                        if (products.ContainsKey(articleNumber))
                        {
                            Product a = products[articleNumber];
                            
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
                                        products.Remove(articleNumber);

                                        WriteLine("Product deleted");
                                       
                                        Thread.Sleep(2000);
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

                        string categoryname, description, imageURL;

                        do
                        {
                            Clear();

                            AddCategory(out categoryname, out description, out imageURL);

                            Write("\nIs this correct Y(es) N(o)");

                        } while (ReadKey(true).Key == ConsoleKey.N);

                        ProductCategory category = new ProductCategory(categoryname, description, imageURL);

                        if (productCategory.ContainsKey(category.Name))
                        {
                            Write("This category already exists");
                        }
                        else
                        {
                            productCategory.Add(category.Name, category);

                            WriteLine("Category added");
                        }
                      
                        Thread.Sleep(2000);
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:

                        Clear();

                        Write("Article number: ");

                        string productArticleNumber = ReadLine();

                        if (products.ContainsKey(productArticleNumber))

                        {
                            Product a = products[productArticleNumber];

                            Write("Category name: ");

                            string categoryName = ReadLine();

                            if (productCategory.ContainsKey(categoryName))
                            {
                                productCategory[categoryName].AddProduct(a);

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

                        foreach (ProductCategory cat in productCategory.Values)
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

                        return;

                }
            }

        }

        /// <summary>
        /// Method populates temporary data
        /// </summary>
        /// <param name="products"></param>
        /// <param name="productCategory"></param>
        static void PopulateData(Dictionary<string, Product> products, Dictionary<string, ProductCategory> productCategory)
        {
            Product temporaryProduct = new()
            {
                ArticleNumber = "123",
                Name = "T-Shirts",
                Price = 200
            };
            products.Add(temporaryProduct.ArticleNumber, temporaryProduct);

            ProductCategory temporaryProductCategory = new("Clothes", "...", "...");
            temporaryProductCategory.Products.Add(temporaryProduct);
            productCategory.Add(temporaryProductCategory.Name, temporaryProductCategory);
        }
        static void Print(Product p)
        {
            WriteLine($"Article number:{p.ArticleNumber}");
            WriteLine($"Name: {p.Name}");
            WriteLine($"Description: {p.Description}");
            WriteLine($"Image URL: {p.ImageURL}");
            WriteLine($"Price: {p.Price}");
        }
        private static void AddCategory(out string categoryname, out string description, out string imageURL)
        {
            WriteLine("Name:");
            WriteLine("Description:");
            WriteLine("Image URL:");

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
            Dictionary<string, Product> products = new();
            Dictionary<string, ProductCategory> productCategory = new();
            PopulateData(products, productCategory);
            

            Dictionary<string, string> userLogin = new();
            userLogin.Add("Tina", "strategi");
            userLogin.Add("Alex", "skydd");

            while (true)
            {
                Authenticate(userLogin);

                DoMainMenu(products, productCategory);
            }
           

        }
    }
        
    
}
        
    

