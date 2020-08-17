using EPiServer.Find;
using EPiServer.Find.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace FindConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BackgroundColor = ConsoleColor.White;
            ForegroundColor = ConsoleColor.Black;
            Clear();

            var client = Client.CreateFromConfig();

            /*
            client.ClearIndex();

            client.Output();
            
            var p1 = new Person
            {
                PersonID = 1,
                FirstName = "Alice",
                LastName = "Smith"
            };

            var response = client.Index(p1);
            response.Output();

            var s1 = new Student
            {
                PersonID = 2,
                FirstName = "Bob",
                LastName = "Jones",
                Course = "CMS Advanced Development"
            };

            response = client.Index(s1, command =>
            {
                command.Refresh = true; // so it appears in results immediately
                command.TimeToLive = TimeSpan.FromMinutes(30);
                command.Id = s1.PersonID; // manually set the DocumentId for the item
            });
            response.Output();
            

            ITypeSearch<Person> results = client.Search<Person>();
            results.Output();

            foreach (Book item in BookRepository.Books)
            {
                client.Index(item, command => 
                    { command.Refresh = true; }).Output();
            }

            ITypeSearch<Book> books = client.Search<Book>();
            books.Output();

            */

            client.SearchBooksFor("lord");
            client.SearchBooksFor("lord of the rings");
            client.SearchBooksFor("\"lord of the rings\"");
            client.SearchBooksFor("stories");
            client.SearchBooksFor("politics");
            client.SearchBooksFor("time and space");
        }
    }
}
