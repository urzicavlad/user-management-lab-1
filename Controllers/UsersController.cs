using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagement.Models;

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

        [HttpPost]
        public User Post(User user)
        {
            _logger.LogInformation("post user called");
            _logger.LogInformation(user.ToString());
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}