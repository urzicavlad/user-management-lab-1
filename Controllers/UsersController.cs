using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagement.Models;
using UserManagement.Validator;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, UsersContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public List<User> GetAll()
        {
            _logger.LogInformation("get-all-users called");
            return _context.Users.ToList();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetBook(long id)
        {
            var user = await _context.Users.FindAsync(id);
            
            if (!UserValidator.IsValid(user))
            {
                return NotFound();
            }

            return user;
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteBook(long id)
        {
            var user = await _context.Users.FindAsync(id);
            
            if (!UserValidator.IsValid(user))
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostBook(User user)
        {
            _logger.LogInformation("post user called");
            if (!UserValidator.IsValid(user))
            {
                return BadRequest();
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}