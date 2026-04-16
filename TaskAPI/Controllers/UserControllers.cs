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
        private readonly ILogger<UserController>_logger;
 
        public UserController(AppDbContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
        }
 
        [HttpGet]
        public IActionResult GetUsers()
        {
            _logger.LogInformation("Fetching all users");
            var users = _context.Users.ToList();

            _logger.LogInformation($"Total users fetched: {users.Count}");
            return Ok(users);
        }
 
        // [HttpPost]
        // public IActionResult AddUser(User user)
        // {
        //     _context.Users.Add(user);
        //     _context.SaveChanges();
        //     return Ok(user);
        // }

 [HttpPost]
public IActionResult AddUser([FromBody] User user)
{
    _logger.LogInformation("Adding new user");
 
    if (!ModelState.IsValid)
    {
        _logger.LogWarning("Invalid user data received");
        return BadRequest(ModelState);
    }
 
    try{
    _context.Users.Add(user);
    _context.SaveChanges();
 
    _logger.LogInformation($"User added with Id: {user.Id}");
 
    // return Ok(user);
    return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    } 
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error while adding user");
        return StatusCode(500, "Internal server error");
    }
}

[HttpDelete("{id}")]
public IActionResult DeleteUser(int id)
{
    _logger.LogInformation($"Deleting user with Id: {id}");
 
    var user = _context.Users.FirstOrDefault(u => u.Id == id);
 
    if (user == null)
    {
        _logger.LogWarning("User not found");
        return NotFound("User not found");
    }
 
    _context.Users.Remove(user);
    _context.SaveChanges();
 
    _logger.LogInformation($"User deleted with Id: {id}");
 
    return Ok("User deleted successfully");
}

[HttpPut("{id}")]
public IActionResult UpdateUser(int id, User updatedUser)
{
    var user = _context.Users.FirstOrDefault(u => u.Id == id);
 
    if (user == null)
        return NotFound("User not found");
 
    user.Name = updatedUser.Name;
    user.Email = updatedUser.Email;
 
    _context.SaveChanges();
 
    return Ok(user);
}

    [HttpGet("{id}")]
public IActionResult GetUserById(int id)
{
    var user = _context.Users.FirstOrDefault(u => u.Id == id);
 
    if (user == null)
        return NotFound();
 
    return Ok(user);
}
    }
}
 