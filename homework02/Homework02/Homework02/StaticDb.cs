using Homework02.Models;

namespace Homework02
{
    public static class StaticDb
    {
        public static List<Book> Books = new List<Book>()
        {
            new Book()
            {
                Author = "Harper Lee",
                Title = "To Kill a Mockingbird"
            },
            new Book()
            {
                Author = "F. Scott Fitzgerald",
                Title = "The Great Gatsby"
            },
            new Book()
            {
                 Author = "George Orwell",
                 Title = "1984"
            },
            new Book()
            {
                Author = "Jane Austen",
                Title = "Pride and Prejudice"
            },
            new Book()
            {
                Author = "Jane Austen",
                Title = "Pride and Prejudice"
            }
        };
    }
}
