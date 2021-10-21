using System;
using System.Collections.Generic;
using static System.Console;
using System.Threading;

namespace ProductManager
{
            class Program
            
            {
            static void Main(string[] args)
            {
                Dictionary<string, Product> products = new Dictionary<string, Product>();            
                Dictionary<string, ProductCategory> productCategory = new Dictionary<string, ProductCategory>();

            /*Product tp = new Product();
            tp.Name = "T-Shirts";
            tp.Price = 200;
            products.Add(tp.Name, tp);

            ProductCategory tpc = new ProductCategory("Clothes", "...", "...");
            tpc.Products.Add(tp);
            productCategory.Add(tpc.Name, tpc);*/

            while (true)
                {
                    WriteLine("1. Add product");
                    WriteLine("2. Search product");
                    WriteLine("3. Add category");
                    WriteLine("4. Add product to category");
                    WriteLine("5. List categories");
                    WriteLine("6. Exit");


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
                                   || input.Key == ConsoleKey.D6 || input.Key == ConsoleKey.NumPad6);                    }
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


                        } while (ReadKey().Key == ConsoleKey.N);

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

                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        
                        Write("Article number:");
                        string articleNumber = ReadLine();
                      

                        if (products.ContainsKey(articleNumber))

                        {
                            Product a = products[articleNumber];
                            WriteLine($"Articel number:{a.ArticleNumber}");
                            WriteLine($"Name: {a.Name}");
                            WriteLine($"Description: {a.Description}");
                            WriteLine($"Image URL: {a.ImageURL}");
                            WriteLine($"Price: {a.Price}");

                            Console.WriteLine("Press the Escape (Esc) key to quit: ");

                            while (Console.ReadKey().Key != ConsoleKey.Escape) ;

                          
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

                            Write("Is this correct Y(es) N(o)");

                        } while (ReadKey().Key == ConsoleKey.N);

                        ProductCategory category = new ProductCategory(categoryname, description, imageURL);

                        if (productCategory.ContainsKey(category.Name))
                        {
                            return;// visas formuläret på nytt, med tomma fält
                            
                        }
                        else
                        {
                            productCategory.Add(category.Name, category);

                            WriteLine("Category added");

                            Thread.Sleep(2000);
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

                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:

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
                                WriteLine("Product not found");
                            }

                            Thread.Sleep(2000);
                            
                            while (Console.ReadKey().Key != ConsoleKey.Escape) ;

                          
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
                            WriteLine(cat.Name+" ("+cat.Products.Count+")");
                           
                            foreach(Product product in cat.Products)
                            {
                                WriteLine("  "+ product.Name + "\t\t" + product.Price);

                            }
                           /* Write( [productCategory.Count] + "\t");*/
                            
                            // TODO:^Вывести имя категории  (cat.Products.Count)
                            // Сделать вложенный цикл по продуктам и вывести их с отступом

                            
                        }

                        while (Console.ReadKey().Key != ConsoleKey.Escape) ;

                        break;

                     case ConsoleKey.D6:
                     case ConsoleKey.NumPad6:
                           
                        return;

                    }
                
                  Clear();
                 
                  }
            }
        }
    }

