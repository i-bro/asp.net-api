using Homework02.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Book>> GetAllBooks([FromQuery] int? index)
        {
            try
            {
                if (index.HasValue)
                {

                    if (index < 0)
                    {
                        return BadRequest("Index cannot be a negative value");
                    }
                    if (index >= StaticDb.Books.Count)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, $"Book wiht index {index} does not exist");
                    }

                    return Ok(StaticDb.Books[index.Value]);
                }
                
                return Ok(StaticDb.Books);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured please contact your administrator");
            }
        }

       [HttpGet("search")]
       public ActionResult<List<Book>> SearchBooks([FromQuery] string? title, string? author)
        {
            try
            {
                if(string.IsNullOrEmpty(title) && string.IsNullOrEmpty(author))
                {
                    return BadRequest("You have to send at least one filter param");
                }

                if (string.IsNullOrEmpty(title))
                {
                    List<Book> filteredBooks = StaticDb.Books.Where(x => x.Author == author).ToList();
                    return Ok(filteredBooks);
                }
                if (string.IsNullOrEmpty(author))
                {
                    List<Book> filterByTitle = StaticDb.Books.Where(x => x.Title == title).ToList();
                    return Ok(filterByTitle);
                }

                List<Book> filteredByAutorAndTitle = StaticDb.Books.Where(x => x.Author == author && x.Title == title).ToList();
                return Ok(filteredByAutorAndTitle);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured please contact your administrator");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Book book)
        {
            try
            {
                StaticDb.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created, "The new book was added");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured please contact your administrator");
            }
        }

        [HttpPost("titles")]
        public IActionResult PostTitles([FromBody] List<Book> books)
        {
            try
            {
                if(books == null || !books.Any())
                {
                    return BadRequest("The book list cannot be empty");
                }

                StaticDb.Books.AddRange(books);
                var titles = StaticDb.Books.Select(x => x.Title).ToList();

                return Ok(titles);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured please contact your administrator");
            }
        }

    }
}
