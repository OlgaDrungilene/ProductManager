using System;
using System.Collections.Generic;
using static System.Console;
using System.Threading;

namespace ProductManager
{
    class Program

    {
        static void Print(Product p)
        {
            WriteLine($"Articel number:{p.ArticleNumber}");
            WriteLine($"Name: {p.Name}");
            WriteLine($"Description: {p.Description}");
            WriteLine($"Image URL: {p.ImageURL}");
            WriteLine($"Price: {p.Price}");
        }
        static void PopulateData(Dictionary<string, Product> products, Dictionary<string, ProductCategory> productCategory)
        {
            Product tp = new Product();
            tp.ArticleNumber = "123";
            tp.Name = "T-Shirts";
            tp.Price = 200;
            products.Add(tp.ArticleNumber, tp);

            ProductCategory tpc = new ProductCategory("Clothes", "...", "...");
            tpc.Products.Add(tp);
            productCategory.Add(tpc.Name, tpc);
        }

        static void Authenticate(Dictionary<string, string> userLogin)
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

                invalidUser = (!userLogin.ContainsKey(username) || userLogin[username] != password);

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

                        Product p = new Product();
                        do
                        {
                            Clear();

                            WriteLine("Article number:");
                            WriteLine("Name:");
                            WriteLine("Description:");
                            WriteLine("Image URL:");
                            WriteLine("Price:");

                            SetCursorPosition(16, 0);
                            p.ArticleNumber = ReadLine();

                            SetCursorPosition(6, 1);
                            p.Name = ReadLine();

                            SetCursorPosition(14, 2);
                            p.Description = ReadLine();

                            SetCursorPosition(12, 3);
                            p.ImageURL = ReadLine();

                            SetCursorPosition(8, 4);
                            p.Price = Convert.ToDecimal(ReadLine());

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
                                        // foreach


                                        WriteLine("Product deleted");
                                        Thread.Sleep(2000);
                                    }
                                    Clear();
                                }
                                else
                                {
                                    // Escape
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
                            WriteLine("Name:");
                            WriteLine("Description:");
                            WriteLine("Image URL:");

                            SetCursorPosition(6, 0);
                            categoryname = ReadLine();

                            SetCursorPosition(12, 1);
                            description = ReadLine();

                            SetCursorPosition(10, 2);
                            imageURL = ReadLine();

                            Write("\nIs this correct Y(es) N(o)");

                        } while (ReadKey(true).Key == ConsoleKey.N);

                        ProductCategory category = new ProductCategory(categoryname, description, imageURL);

                        if (productCategory.ContainsKey(category.Name))
                        {
                            Write("This category already exists");// visas formuläret på nytt, med tomma fält

                        }
                        else
                        {
                            productCategory.Add(category.Name, category);

                            WriteLine("Category added");

                        }
                        /*
                         1. Добавьте пункт 3. ADd Category  в основное меню (3-ий Exit станет четврётым)
                            Добавьте словарь категорий Dictionary<String, ProductCategory> ... 
                         2. По аналогии сделать добавлени категории (как продукта) 
                             string name = ReadLine();
                        ¨   string url = REadLine();
                           ...
                        ¨  ProductCatgory category = new ProductCategory(name, url, ... )
                         */
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

                        // TODO: Вывести шапку таблицы (2 строки)


                        WriteLine("Name                         Price");
                        WriteLine("-------------------------------------------------");

                        foreach (ProductCategory cat in productCategory.Values)
                        {
                            WriteLine(cat.Name + " (" + cat.Products.Count + ")");

                            foreach (Product product in cat.Products)
                            {
                                WriteLine("  " + product.Name + "\t\t" + product.Price);

                            }
                            /* Write( [productCategory.Count] + "\t");*/

                            // TODO:^Вывести имя категории  (cat.Products.Count)
                            // Сделать вложенный цикл по продуктам и вывести их с отступом


                        }
                        WaitForEscape();

                        break;

                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:

                        return;

                }
            }

        }

        static void Main(string[] args)
        {
            Dictionary<string, Product> products = new Dictionary<string, Product>();
            Dictionary<string, ProductCategory> productCategory = new Dictionary<string, ProductCategory>();
            PopulateData(products, productCategory);

            Dictionary<string, string> userLogin = new Dictionary<string, string>();
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
        
    

