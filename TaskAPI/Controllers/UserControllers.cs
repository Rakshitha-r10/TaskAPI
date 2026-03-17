using Microsoft.AspNetCore.Mvc;
using TaskAPI.Models;
using TaskAPI.Data;
 
namespace TaskAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
 
        public UserController(AppDbContext context)
        {
            _context = context;
        }
 
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }
 
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }
    }
}
 