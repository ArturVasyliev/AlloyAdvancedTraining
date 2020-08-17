using System;
using System.Collections.Generic;
using System.Globalization;

namespace FindConsole
{
    public class Book
    {
        [EPiServer.Find.Id]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime? Published { get; set; }
        public int? PageCount { get; set; }
    }

    public static class BookRepository
    {
        // information about the books is from http://thegreatestbooks.org/

        public static IEnumerable<Book> Books { get; private set; }

        static BookRepository()
        {
            var books = new HashSet<Book>();

            books.Add(new Book
            {
                Id = 1,
                Title = "The Lord of the Rings",
                Author = "J. R. R. Tolkien ",
                Description = "The Lord of the Rings is an epic high fantasy novel written by philologist and Oxford University professor J. R. R. Tolkien. The story began as a sequel to Tolkien's earlier, less complex children's fantasy novel The Hobbit (1937), but eventually developed into a much larger work. It was written in stages between 1937 and 1949, much of it during World War II. Although generally known to readers as a trilogy, the work was initially intended by Tolkien to be one volume of a two-volume set along with The Silmarillion; however, the publisher decided to omit the second volume and instead published The Lord of the Rings in 1954-55 as three books rather than one, for economic reasons. It has since been reprinted numerous times and translated into many languages, becoming one of the most popular and influential works in 20th-century literature.",
                Published = DateTime.Parse("2 August 1995", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 1178
            });

            books.Add(new Book
            {
                Id = 2,
                Title = "In Search of Lost Time",
                Author = "Marcel Proust",
                Description = "Swann's Way, the first part of A la recherche de temps perdu, Marcel Proust's seven-part cycle, was published in 1913. In it, Proust introduces the themes that run through the entire work. The narrator recalls his childhood, aided by the famous madeleine; and describes M. Swann's passion for Odette. The work is incomparable. Edmund Wilson said \"[Proust] has supplied for the first time in literature an equivalent in the full scale for the new theory of modern physics.\"",
                Published = DateTime.Parse("2 October 2003", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 720
            });

            books.Add(new Book
            {
                Id = 3,
                Title = "Ulysses",
                Author = "James Joyce",
                Description = "Ulysses chronicles the passage of Leopold Bloom through Dublin during an ordinary day, June 16, 1904. The title parallels and alludes to Odysseus (Latinised into Ulysses), the hero of Homer's Odyssey (e.g., the correspondences between Leopold Bloom and Odysseus, Molly Bloom and Penelope, and Stephen Dedalus and Telemachus). Joyce fans worldwide now celebrate June 16 as Bloomsday.",
                Published = DateTime.Parse("17 December 1992", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 1076
            });

            books.Add(new Book
            {
                Id = 4,
                Title = "Moby Dick",
                Author = "Herman Melville",
                Description = "First published in 1851, Melville's masterpiece is, in Elizabeth Hardwick's words, \"the greatest novel in American literature.\" The saga of Captain Ahab and his monomaniacal pursuit of the white whale remains a peerless adventure story but one full of mythic grandeur, poetic majesty, and symbolic power. Filtered through the consciousness of the novel's narrator, Ishmael, Moby-Dick draws us into a universe full of fascinating characters and stories, from the noble cannibal Queequeg to the natural history of whales, while reaching existential depths that excite debate and contemplation to this day.",
                Published = DateTime.Parse("5 May 1992", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 544
            });

            books.Add(new Book
            {
                Id = 5,
                Title = "Hamlet",
                Author = "William Shakespeare",
                Description = "The Tragedy of Hamlet, Prince of Denmark, or more simply Hamlet, is a tragedy by William Shakespeare, believed to have been written between 1599 and 1601. The play, set in Denmark, recounts how Prince Hamlet exacts revenge on his uncle Claudius, who has murdered Hamlet's father, the King, and then taken the throne and married Gertrude, Hamlet's mother. The play vividly charts the course of real and feigned madness—from overwhelming grief to seething rage—and explores themes of treachery, revenge, incest, and moral corruption.",
                Published = DateTime.Parse("11 August 2016", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 232
            });

            books.Add(new Book
            {
                Id = 6,
                Title = "War and Peace",
                Author = "Leo Tolstoy",
                Description = "Epic in scale, War and Peace delineates in graphic detail events leading up to Napoleon's invasion of Russia, and the impact of the Napoleonic era on Tsarist society, as seen through the eyes of five Russian aristocratic families.",
                Published = DateTime.Parse("29 October 1992", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 1744
            });

            books.Add(new Book
            {
                Id = 7,
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                Description = "The novel chronicles an era that Fitzgerald himself dubbed the \"Jazz Age\". Following the shock and chaos of World War I, American society enjoyed unprecedented levels of prosperity during the \"roaring\" 1920s as the economy soared. At the same time, Prohibition, the ban on the sale and manufacture of alcohol as mandated by the Eighteenth Amendment, made millionaires out of bootleggers and led to an increase in organized crime, for example the Jewish mafia. Although Fitzgerald, like Nick Carraway in his novel, idolized the riches and glamor of the age, he was uncomfortable with the unrestrained materialism and the lack of morality that went with it, a kind of decadence.",
                Published = DateTime.Parse("1 September 2012", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 192
            });

            books.Add(new Book
            {
                Id = 8,
                Title = "The Adventures of Huckleberry Finn",
                Author = "Mark Twain",
                Description = "Revered by all of the town's children and dreaded by all of its mothers, Huckleberry Finn is indisputably the most appealing child-hero in American literature. Unlike the tall-tale, idyllic world of Tom Sawyer, The Adventures of Huckleberry Finn is firmly grounded in early reality. From the abusive drunkard who serves as Huckleberry's father, to Huck's first tentative grappling with issues of personal liberty and the unknown, Huckleberry Finn endeavors to delve quite a bit deeper into the complexities — both joyful and tragic of life.",
                Published = DateTime.Parse("28 June 2007", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 320
            });

            books.Add(new Book
            {
                Id = 9,
                Title = "Anna Karenina",
                Author = "Leo Tolstoy",
                Description = "Anna Karenina tells of the doomed love affair between the sensuous and rebellious Anna and the dashing officer, Count Vronsky. Tragedy unfolds as Anna rejects her passionless marriage and must endure the hypocrisies of society. Set against a vast and richly textured canvas of nineteenth-century Russia, the novel's seven major characters create a dynamic imbalance, playing out the contrasts of city and country life and all the variations on love and family happiness. While previous versions have softened the robust, and sometimes shocking, quality of Tolstoy's writing, Pevear and Volokhonsky have produced a translation true to his powerful voice. This award-winning team's authoritative edition also includes an illuminating introduction and explanatory notes. Beautiful, vigorous, and eminently readable, this Anna Karenina will be the definitive text for generations to come.",
                Published = DateTime.Parse("5 October 1995", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 848
            });

            books.Add(new Book
            {
                Id = 10,
                Title = "Great Expectations",
                Author = "Charles Dickens",
                Description = "Great Expectations is written in the genre of \"bildungsroman\" or the style of book that follows the story of a man or woman in their quest for maturity, usually starting from childhood and ending in the main character's eventual adulthood. Great Expectations is the story of the orphan Pip, writing his life from his early days of childhood until adulthood and trying to be a gentleman along the way. The story can also be considered semi-autobiographical of Dickens, like much of his work, drawing on his experiences of life and people.",
                Published = DateTime.Parse("7 September 2012", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 512
            });

            books.Add(new Book
            {
                Id = 11,
                Title = "David Copperfield",
                Author = "Charles Dickens",
                Description = "The story of the abandoned waif who learns to survive through challenging encounters with distress and misfortune.",
                Published = DateTime.Parse("10 February 2012", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 928
            });

            books.Add(new Book
            {
                Id = 12,
                Title = "Nineteen Eighty Four",
                Author = "George Orwell",
                Description = "The story follows the life of one seemingly insignificant man, Winston Smith, a civil servant assigned the task of perpetuating the regime's propaganda by falsifying records and political literature so that it appears that the government is always correct in what it says. Smith grows disillusioned with his meager existence and so begins a rebellion against the system that leads to his arrest, torture, and conversion.",
                Published = DateTime.Parse("29 October 1992", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 326
            });

            books.Add(new Book
            {
                Id = 13,
                Title = "Alice's Adventures in Wonderland",
                Author = "Lewis Carroll",
                Description = "In 1862 Charles Lutwidge Dodgson, a shy Oxford mathematician with a stammer, created a story about a little girl tumbling down a rabbit hole. Thus began the immortal adventures of Alice, perhaps the most popular heroine in English literature. Countless scholars have tried to define the charm of the Alice books–with those wonderfully eccentric characters the Queen of Hearts, Tweedledum, and Tweedledee, the Cheshire Cat, Mock Turtle, the Mad Hatter et al.–by proclaiming that they really comprise a satire on language, a political allegory, a parody of Victorian children’s literature, even a reflection of contemporary ecclesiastical history. Perhaps, as Dodgson might have said, Alice is no more than a dream, a fairy tale about the trials and tribulations of growing up–or down, or all turned round–as seen through the expert eyes of a child.",
                Published = DateTime.Parse("4 May 2017", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 160
            });

            books.Add(new Book
            {
                Id = 14,
                Title = "Bleak House",
                Author = "Charles Dickens",
                Description = "Bleak House is the ninth novel by Charles Dickens, published in twenty monthly instalments between March 1852 and September 1853. It is held to be one of Dickens's finest novels, containing one of the most vast, complex and engaging arrays of minor characters and sub-plots in his entire canon. The story is told partly by the novel's heroine, Esther Summerson, and partly by omniscient narrator. Memorable characters include the menacing lawyer Tulkinghorn, the friendly but depressive John Jarndyce and the childish Harold Skimpole.",
                Published = DateTime.Parse("26 September 1991", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 880
            });

            books.Add(new Book
            {
                Id = 15,
                Title = "Hunger",
                Author = "Knut Hamsun",
                Description = "Hunger is a novel by the Norwegian author Knut Hamsun and was published in its final form in 1890. Parts of it had been published anonymously in the Danish magazine Ny Jord in 1888. The novel is hailed as the literary opening of the 20th century and an outstanding example of modern, psychology-driven literature. It hails the irrationality of the human mind in an intriguing and sometimes humorous novel. It has been translated into English three times: in 1899 by Mary Chavelita Dunne (under the alias George Egerton), by Robert Bly in 1967, and by Sverre Lyngstad, whose translation is considered definitive. Written after Hamsun's return from an ill-fated tour of America, Hunger is loosely based on the author's own impoverished life before his breakthrough in 1890. Set in late 19th century Kristiania, the novel recounts the adventures of a starving young man whose sense of reality is giving way to a delusionary existence on the darker side of a modern metropolis. While he vainly tries to maintain an outer shell of respectability, his mental and physical decay are recounted in detail. His ordeal, enhanced by his inability or unwillingness to pursue a professional career, which he deems unfit for someone of his abilities, is pictured in a series of encounters which Hamsun himself described as 'a series of analyses.'",
                Published = DateTime.Parse("5 June 2016", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 178
            });

            books.Add(new Book
            {
                Id = 16,
                Title = "The Hunger Games",
                Author = "Suzanne Collins",
                Description = "The Hunger Games is a trilogy of young adult dystopian novels written by American novelist Suzanne Collins. The series is set in The Hunger Games universe, and follows young characters Katniss Everdeen and Peeta Mellark.",
                Published = DateTime.Parse("20 October 2008", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 374
            });

            books.Add(new Book
            {
                Id = 17,
                Title = "Lord of the Flies",
                Author = "William Golding",
                Description = "Lord of the Flies discusses how culture created by man fails, using as an example a group of British schoolboys stuck on a deserted island who try to govern themselves, but with disastrous results. Its stances on the already controversial subjects of human nature and individual welfare versus the common good.",
                Published = DateTime.Parse("3 March 1997", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 225
            });

            books.Add(new Book
            {
                Id = 18,
                Title = "Lords and Ladies",
                Author = "Terry Pratchett",
                Description = "Lords and Ladies is the fourteenth Discworld book. It was originally published in 1992. Much of the storyline spoofs elements of Shakespeare's play A Midsummer Night's Dream.",
                Published = DateTime.Parse("3 July 2014", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 336
            });

            books.Add(new Book
            {
                Id = 19,
                Title = "Ring",
                Author = "Koji Suzuki",
                Description = "Ring (リング Ringu?) is a Japanese mystery horror novel, first published in 1991, and set in modern-day Japan. It was the basis for a 1995 film (Ring: Kanzenban), a television series (Ring: The Final Chapter), a film of the same name (1998's Ring), and two remakes of the 1998 film: a South Korean version (The Ring Virus) and an American version (The Ring).",
                Published = DateTime.Parse("1 July 2003", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 286
            });

            books.Add(new Book
            {
                Id = 20,
                Title = "The Ringing Cedars of Russia",
                Author = "Vladimir Megre",
                Description = "The second book of the Ringing Cedars Series, in addition to providing a fascinating behind-the-scenes look at the story of how \"Anastasia\" came to be published, offers a deeper exploration of the universal concepts so dramatically revealed in Book 1. It takes the reader on an adventure through the vast expanses of space, time and spirit from the Paradise-like glade in the Siberian taiga to the rough urban depths of Russia's capital city, from the ancient mysteries of our forebears to a vision of humanity's radiant future.",
                Published = DateTime.Parse("24 December 2013", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 226
            });

            books.Add(new Book
            {
                Id = 21,
                Title = "C# 7 and .NET Core: Modern Cross-Platform Development",
                Author = "Mark J. Price",
                Description = "C# has recently been made open source and now supports cross-platform development for Linux, macOS, and Windows. It can be used to create everything from business applications, websites, and services to games for Android and iOS mobile phones and Xbox One. If you want to build powerful cross-platform applications with C# 7 and .Net Core, then this book is for you.",
                Published = DateTime.Parse("24 March 2017", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 594
            });

            books.Add(new Book
            {
                Id = 22,
                Title = "C# 7.1 and .NET Core 2.0: Modern Cross-Platform Development",
                Author = "Mark J. Price",
                Description = "Build modern, cross-platform applications with .NET Core 2.0. Get up to speed with C#, and up to date with all the latest features of C# 7.1. Start creating professional web applications with ASP.NET Core 2.0.",
                Published = DateTime.Parse("30 November 2017", CultureInfo.GetCultureInfo("en-GB")),
                PageCount = 800
            });

            BookRepository.Books = books;
        }
    }
}