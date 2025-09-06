using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace LINQtoObject
{
    #region class used to store grouping result
    public class GroupingResultClass
    {
        public string SubjectName { get; set; }
        public List<string> BooksNames { get; set; }
    }
    #endregion
    class Program
    {
        #region printing functions
        public static void Print<T>(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("---------------------------------------------------");
        }

        public static void PrintGrouping(IEnumerable<GroupingResultClass> query)
        {
            foreach (var item in query)
            {
                Console.WriteLine($"================ {item.SubjectName} ================");
                foreach (var bookName in item.BooksNames)
                {
                    Console.WriteLine(bookName);
                }
                Console.WriteLine("---------------------------------------------------");
            }
        }
        #endregion

        #region bounsFunction
        //9-Bouns
        public static void FindBooksSorted(string publisherName, string sortingMethod, bool ascending)
        {
            Func<Book, object> sortKey;

            switch (sortingMethod.ToLower())
            {
                case "title":
                    sortKey = b => b.Title;
                    break;
                case "price":
                    sortKey = b => b.Price;
                    break;
                case "author":
                    sortKey = b => b.Authors;
                    break;
                default:
                    Console.WriteLine("Invalid sorting Method, defaulting to Title.");
                    sortKey = b => b.Title;
                    break;
            }

            var varyPublisher = SampleData.Books.Where(b => b.Publisher.Name.Equals(publisherName,
                                                     StringComparison.OrdinalIgnoreCase));
            var bouns = ascending ? varyPublisher.OrderBy(sortKey) : varyPublisher.OrderByDescending(sortKey);
            foreach (var book in bouns)
            {
                Console.Write($"Title: {book.Title}, Price: {book.Price}, Authors: ");

                foreach (var au in book.Authors)
                    Console.Write($"{au.FirstName} {au.LastName}, ");

                Console.WriteLine($"Publisher: {book.Publisher.Name}");
            }
        }
        #endregion
        static void Main(string[] args)
        {
            #region QList
            List<string> Questions = new List<string>()
            {
                "1-Display book title and its ISBN.",
                "2-Display the first 3 books that cost more than 25.",
                "3-Display Book title along with its publisher name. (Using 2 methods).",
                "4-Find the number of books which cost more than 20.",
                "5-Display book title, price and subject name sorted by its subject name ascending and by its price descending.",
                "6-Display All subjects with books related to this subject. (Using 2 methods).",
                "7-Try to display book title & price (from book objects) returned from GetBooks Function.",
                "8-Display books grouped by publisher & Subject.",
                "9-Bouns: 1-Ask the user for a publisher name & sorting method " +
                "(sorting criteria (by Title, price, etc….) and sorting way " +
                "(ASC. Or DESC.))…. And implement a function named FindBooksSorted() that" +
                " displays all books written by this Author sorted as the user requested."
            };
            Print(Questions);
            #endregion

            #region Q&S
            // 1-Display book title and its ISBN.
            var result1 = SampleData.Books.Select(c => new { c.Title, c.Isbn });

            // 2-Display the first 3 books that cost more than 25.
            var result2 = SampleData.Books.Where(c => c.Price > 25).Take(3);

            //3-Display Book title along with its publisher name. (Using 2 methods)
                //With Query Operator
            var result3_1 = SampleData.Books.Select(s => new { s.Title, publisherName = s.Publisher.Name } ) ;
                //With Query Expression
            var result3_2 = from n in SampleData.Books
                            select new { n.Title, PublisherName = n.Publisher.Name };

            //4-Find the number of books which cost more than 20.
            var result4 = SampleData.Books.Where(c => c.Price > 20).Count();

            //5-Display book title, price and subject name sorted by its subject name ascending and by its price descending.
            var result5 = from n in SampleData.Books
                          orderby n.Subject.Name, n.Price descending
                          select new { n.Title, n.Price, SubjectName = n.Subject.Name };

            //6-Display All subjects with books related to this subject. (Using 2 methods).
            //subquery
            var books = SampleData.Books;
            var subjects = SampleData.Subjects;
               //SubQuery
            var result6_1 = from sub in subjects
                            select new GroupingResultClass
                            {
                                SubjectName = sub.Name,
                                BooksNames =
                                    (from book in books
                                     where book.Subject.Name == sub.Name
                                     select book.Title).ToList()
                            };

            ////GroupByInto
            var result6_2 = from book in books
                            group book by book.Subject.Name into g
                            select new GroupingResultClass
                            {
                                SubjectName = g.Key,
                                BooksNames = g.Select(b => b.Title).ToList()
                            };

            //"7-Try to display book title & price (from book objects) returned from GetBooks Function.
            //solved in Case 7 in switch

            //8-Display books grouped by publisher & Subject.
            var result8 = from book in books
                          group book by new { PublisherName = book.Publisher.Name, SubjectName = book.Subject.Name } into gr
                          select new
                          {
                              PublisherName = gr.Key.PublisherName,
                              SubjectName = gr.Key.SubjectName,        
                              Books = gr.ToList()
                          };

            #endregion

            #region switch

            int choose;
            Console.WriteLine("Enter the number of the question to desplay the result");
            choose = Convert.ToInt32(Console.ReadLine());

            switch (choose)
            {
                case 1: Print(result1); break;
                case 2: Print(result2); break;
                case 3:
                        Console.WriteLine("With Query Operator");
                        Print(result3_1);
                        Console.WriteLine("With Query Expression");
                        Print(result3_2); 
                    break;
                case 4: Console.WriteLine(result4); break;
                case 5: Print(result5); break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("************************* with subquery *************************");
                    Console.ResetColor();
                    PrintGrouping(result6_1);

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("************************* with GroupBy *************************");
                    Console.ResetColor();                   
                    PrintGrouping(result6_2);

                    break;
                case 7:
                    //"7-Try to display book title & price (from book objects) returned from GetBooks Function.
                    ArrayList result7 = SampleData.GetBooks();
                    foreach (Book item in result7)
                    {
                        Console.WriteLine($"{item.Title} => {item.Price}");
                    }
                    break;
                case 8:
                    foreach (var group in result8)
                    {
                        Console.WriteLine($"Publisher: {group.PublisherName}, Subject: {group.SubjectName}");
                        foreach (var b in group.Books)
                        {
                            Console.WriteLine($"   - {b.Title} ({b.Price})");
                        }
                        Console.WriteLine("------------------------------");
                    }
                    break;
                case 9:
                    Console.WriteLine("Please type the word (you can ignore upper/lower case).");
                    Console.Write("Enter Publisher Name: ");
                    string pub = Console.ReadLine();

                    Console.Write("Enter Sorting Criteria (Title / Price / Author): ");
                    string sort = Console.ReadLine();
                    
                    Console.Write("Enter Sorting Way (1 for ASC / 0 for DESC): ");
                    string input = Console.ReadLine();

                    bool asc = input == "1"; 

                    Console.WriteLine("\n--- Result ---");
                    FindBooksSorted(pub, sort, asc);
                    break;

                default: Console.WriteLine("invalid number"); break;
            };
            #endregion
        }
    }
}