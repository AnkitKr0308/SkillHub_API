using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using skillhub_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Security.Policy;
using MySql.Data.MySqlClient;
using skillhub_api.DTO;

namespace skillhub_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("getemails")]
        public async Task<ActionResult<Users>> GetEmails ()
        {
            var emails = await _context.Users.Select(user => user.Email).ToListAsync();

            return Ok(emails);
        }

        [HttpPost("signup")]
        public async Task <ActionResult> CreateUser (UserCredDTO user)
        {

            var userData = new Users
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleId = user.RoleId
            };

            _context.Users.Add(userData);
            await _context.SaveChangesAsync();

            string pwd = user.Password;
            string hashedPwd = BCrypt.Net.BCrypt.HashPassword(pwd);


            var cred = new Creds { UserId = userData.UserId, Password = hashedPwd };
            _context.Creds.Add(cred);


            await _context.SaveChangesAsync(); 
            return Ok(userData);
        }

        [HttpPost("login")]
        public async Task <ActionResult> ValidateUserCreds ([FromBody] LoginDTO model)
        {
            var emailParam = new MySqlParameter("@emailParam", model.email);

           var result = await _context.Users.FromSqlRaw("CALL sp_getUserCreds (@emailParam)", emailParam).ToListAsync();

            var user = result.FirstOrDefault();

            if (user == null)
            {
                return NotFound("User Not Found");
            }

            var cred = await _context.Creds.FirstOrDefaultAsync(c => c.UserId == user.UserId);

            bool isPwdValid = BCrypt.Net.BCrypt.Verify(model.Password, cred.Password);

            if (!isPwdValid)
            {
                return Unauthorized("Invalid Credentials");
            }

            return Ok(user);
        }

       
    }
}
