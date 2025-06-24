using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skillhub_api.Models;

namespace skillhub_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RolesController (AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task <ActionResult<Roles>> GetRoles()
        {
            var roles = await _context.Roles.Select(roles => new { roles.ID, roles.Role }).ToListAsync();

            return Ok(roles);

        }
    }
}
