using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UsersApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<string>> GetUsers()
        {
            return Ok(StaticDb.Users);
        }

        [HttpGet("{index}")]
        public ActionResult<string> GetByIndex(int index)
        {
            try
            {
                if(index < 0)
                {
                    return BadRequest("The index cannot be negative value");
                }
                if(index >= StaticDb.Users.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"User with index {index} does not exist");
                }

                return Ok(StaticDb.Users[index]);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured please try later");
            }
        }
    }
}
