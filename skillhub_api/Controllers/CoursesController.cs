using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skillhub_api.Models;

namespace skillhub_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CoursesController (AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetCourses()
        {
           var courses = await _context.Courses.Select(c=>new {c.CourseId, c.Title, c.Description }).ToListAsync();
            return Ok(courses);
        }

        [HttpPost("AddCourse")]
        public async Task <ActionResult> AddCourse(Courses course)
        {
            _context.Courses.Add(course);
            var courseData = await _context.SaveChangesAsync();

            if (courseData>0)
            {
                return Ok(course);
            }
            else
            {
                return BadRequest("Course Data Not Added");
            }
        }
    }
}
