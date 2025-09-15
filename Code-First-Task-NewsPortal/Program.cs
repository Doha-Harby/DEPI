using Code_First_Task_NewsPortal.Models;

namespace Code_First_Task_NewsPortal
{
    internal class Program
    {
        static void Main(string[] args)
        {    
            while (true)
            {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("to test login use Email: john@example.com Password: 1234");
            Console.ResetColor();

                Console.WriteLine("Enter \n1 => Create News \n2 => Read News \n3 => Ubdate News \n4 => Delete News \n5 => Show Others News \n6 => Exit");
                string input = Console.ReadLine();

                if (input == "6" )
                {
                    Console.WriteLine("Exiting... Goodbye!");
                    break;
                }

                if (int.TryParse(input, out int cho))
                {
                    Action action = cho switch
                    {
                        1 => AddNews,
                        2 => ReadNews,
                        3 => UpdateNews,
                        4 => DeleteNews,
                        5 => ShowOthersNews,
                        _ => () => Console.WriteLine(" Invalid choice")
                    };

                    action();
                }
            }
        }

        private static Author? LogIn()
        {
            using (var db = new NewsPortalContext())
            {
                    Console.Write("Enter Email: ");
                var email = Console.ReadLine();

                Console.Write("Enter Password: ");
                var password = Console.ReadLine();

                var author = db.Authors.FirstOrDefault(a => a.Email == email && a.Password == password);

                if (author == null)
                {
                    Console.WriteLine("Login failed. Wrong email or password.");
                    return null;
                }
                else
                {
                    Console.WriteLine($"Welcome {author.Name}!");
                    return author;
                } 
            }
        }

        private static void AddNews()
        {
            using (var db = new NewsPortalContext())
            {
                var author = LogIn();
                if (author == null) return;

                Console.Write("Enter News Title: ");
                var title = Console.ReadLine();

                Console.Write("Enter News Description: ");
                var desc = Console.ReadLine();

                Console.Write("Enter Category Id: ");
                int catId = int.Parse(Console.ReadLine());

                var news = new News
                {
                    Title = title ?? string.Empty,
                    Description = desc,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Time = DateTime.Now.TimeOfDay,
                    AuthorId = author.Id,
                    CatalogId = catId
                };

                db.News.Add(news);
                db.SaveChanges();

                Console.WriteLine("News added successfully!");
                
            }
        }

        private static void ReadNews()
        {
            using (var db = new NewsPortalContext())
            {
                var author = LogIn();
                if (author == null) return;

                var ReNews = db.News.Where(n => n.AuthorId == author.Id);

                if (ReNews == null)
                {
                    Console.WriteLine("News not found or not your news.");
                    return;
                }

                var nw = ReNews.ToList();
                Console.WriteLine("===== Aurhor's News =====");
                foreach (var item in nw)
                {
                    Console.WriteLine(item);
                }
                
            }
        }

        private static void UpdateNews()
        {
            using (var db = new NewsPortalContext())
            {
                var author = LogIn();
                if (author == null) return;

                Console.Write("Enter News Id: ");
                int UpNewsId = int.Parse(Console.ReadLine());

                var UpNews = db.News.FirstOrDefault(n => n.Id == UpNewsId && n.AuthorId == author.Id);

                if (UpNews == null)
                {
                    Console.WriteLine("❌ News not found or not your news.");
                    return;
                }


                Console.WriteLine($"Current Title: {UpNews.Title}");
                Console.Write("Enter new Title (or press Enter to keep): ");
                string newTitle = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newTitle))
                    UpNews.Title = newTitle;

                Console.WriteLine($"Current Description: {UpNews.Description}");
                Console.Write("Enter new Description (or press Enter to keep): ");
                string newDesc = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newDesc))
                    UpNews.Description = newDesc;

                Console.WriteLine($"Current Catalog Id: {UpNews.CatalogId}");
                Console.Write("Enter new Catalog Id (or press Enter to keep): ");
                string newCatInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newCatInput))
                    UpNews.CatalogId = int.Parse(newCatInput);

                    
                UpNews.Date = DateOnly.FromDateTime(DateTime.Now);
                UpNews.Time = DateTime.Now.TimeOfDay;

                db.SaveChanges();
                Console.WriteLine("News updated successfully!");

                
            }
        }

        private static void DeleteNews()
        {
            using (var db = new NewsPortalContext())
            {

                var author = LogIn();
                if (author == null) return;

                Console.Write("Enter News Id: ");
                int DeNewsId = int.Parse(Console.ReadLine());

                var DeNews = db.News.FirstOrDefault(n => n.Id == DeNewsId && n.AuthorId == author.Id);

                if (DeNews == null)
                {
                    Console.WriteLine(" News not found or not your news.");
                    return;
                }

                db.News.Remove(DeNews);
                db.SaveChanges();
                Console.WriteLine("News deleted successfully!");
            }
        }

        private static void ShowOthersNews()
        {
            using var db = new NewsPortalContext();
            {
                var author = LogIn();
                if (author == null) return;

                var othersNews = db.News.Where(n => n.AuthorId != author.Id);
                                  
                if (!othersNews.Any())
                {
                    Console.WriteLine("No news from other authors.");
                    return;
                }

                Console.WriteLine("===== Others' News =====");
                foreach (var n in othersNews)
                {
                    Console.WriteLine(n);
                }
            }
        }

    }
}
