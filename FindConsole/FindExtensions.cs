using static System.Console;
using EPiServer.Find;
using System.Linq;
using EPiServer.Find.Api;
using System;
using EPiServer.Find.Api.Querying;

namespace FindConsole
{
    public static class FindExtensions
    {
        public static void Output(this IClient client)
        {
            WriteLine($"Default index: {client.DefaultIndex}");
            WriteLine($"Service URL: {client.ServiceUrl}");
            WriteLine($"Admin: {client.Settings.Admin}");
            WriteLine($"Connectors: {client.Settings.Connectors}");
            WriteLine($"Max. documents: {client.Settings.MaxDocs}");
            WriteLine($"Statistics: {client.Settings.Stats}");
            WriteLine($"Version: {client.Settings.Version}");
            WriteLine($"Status: {client.GetSettings().Status}");

            string[] languages = client.Settings.Languages.Select(l => l.Name).ToArray();
            WriteLine($"Supports these languages: {string.Join(", ", languages)}");
        }

        public static void Output(this IndexResult result)
        {
            WriteLine($"Index result [OK: {result.Ok}, Index: {result.Index}, Type: {result.Type}, Id: {result.Id}]");
        }

        public static void Output<T>(this ITypeSearch<T> query)
        {
            int pageSize = 10;

            var pagedQuery = query.Take(pageSize);

            SearchResults<T> results = null;

            try
            {
                results = pagedQuery.GetResult();
            }
            catch (Exception ex)
            {
                WriteLine($"{ex.GetType()} says {ex.Message}");
                return;
            }

            int numberOfPages = (int)((double)(results.TotalMatching - 1) / pageSize) + 1;

            WriteLine($"Total matching: {results.TotalMatching}");
            WriteLine($"Server duration: {results.ProcessingInfo.ServerDuration}");
            WriteLine($"Shards [Successful: {results.ProcessingInfo.Shards.Successful}, Failed: {results.ProcessingInfo.Shards.Failed}]");
            WriteLine($"Timed out: {results.ProcessingInfo.TimedOut}");
            WriteLine();

            if (results.TotalMatching == 0)
            {
                WriteLine("There are no results to output.");
            }
            else
            {
                for (int page = 1; page <= numberOfPages; page++)
                {
                    WriteLine($"[ Page {page} of {numberOfPages} ]");
                    WriteLine();
                    OutputPageOfResults(results);
                    pagedQuery = query.Skip(page * pageSize).Take(pageSize);

                    try
                    {
                        results = pagedQuery.GetResult();
                    }
                    catch (Exception ex)
                    {
                        WriteLine($"{ex.GetType()} says {ex.Message}");
                        return;
                    }
                }
            }
        }

        private static void OutputPageOfResults<T>(SearchResults<T> results)
        {
            foreach (SearchHit<T> hit in results.Hits)
            {
                Write($"Type: {hit.Type}, ");
                Write($"Score: {hit.Score}, ");
                if (hit.Document is Book)
                {
                    var b = hit.Document as Book;
                    WriteLine($"Book ID: {b.Id}, Title: {b.Title}, Author: {b.Author}, Description: {b.Description}");
                }
                else if (hit.Document is Person)
                {
                    var p = hit.Document as Person;
                    var s = hit.Document as Student;

                    if (s != null) Write("Student"); else Write("Person");
                    Write($" ID: {p.PersonID}, Full name: {p.FirstName} {p.LastName}");

                    if (s != null) Write($", Course: {s.Course}");

                    WriteLine();
                }
                //else if (hit.Document is Category)
                //{
                //    var c = hit.Document as Category;
                //    WriteLine($"Category ID: {c.CategoryID}, Name: {c.CategoryName}, Description: {c.Description}");
                //}
                //else if (hit.Document is Product)
                //{
                //    var p = hit.Document as Product;
                //    WriteLine($"Product ID: {p.ProductID}, Name: {p.ProductName}, Price: {p.UnitPrice}, Discontinued: {p.Discontinued}, Category: {p.Category?.CategoryName}, Supplier: {p.Supplier?.CompanyName}");
                //}
                else
                {
                    WriteLine($"unknown type: {hit.Type}");
                }
                WriteLine();
            }
            WriteLine();
        }

        public static void ClearIndex(this IClient client)
        {
            client.Delete<object>(x => 
                x.ToString().Exists() | !x.ToString().Exists());
        }

        public static void SearchBooksFor(this IClient client, string q)
        {
            WriteLine($"* Search books for: {q}");

            // only look for "books" and allow word stemming
            ITypeSearch<Book> search = client.Search<Book>(Language.English);

            // perform a "free text" query looking in Title and Description
            IQueriedSearch<Book> query = search.For(q)
                .InField(x => x.Title, 3.0) // triple the weighting for matches in Title
                .InField(x => x.Description);

            (query as ITypeSearch<Book>).Output();
        }
    }
}
