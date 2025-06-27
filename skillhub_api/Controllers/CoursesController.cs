using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MySql.Data.MySqlClient;
using skillhub_api.DTO;
using skillhub_api.Models;

namespace skillhub_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CoursesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetCourses()
        {
            var courses = await _context.Courses.Select(c => new { c.CourseId, c.Title, c.Description }).ToListAsync();
            return Ok(courses);
        }

        [HttpPost("AddCourse")]
        public async Task<ActionResult> AddCourse(Courses course)
        {
            _context.Courses.Add(course);
            var courseData = await _context.SaveChangesAsync();

            if (courseData > 0)
            {
                return Ok(course);
            }
            else
            {
                return BadRequest("Course Data Not Added");
            }
        }

        [HttpGet("EnrolledCourses")]
        public async Task<ActionResult> GetEnrolledCourses([FromQuery] int UserId)
        {
            var user = new MySqlParameter("@UserID", UserId);


            var getCourse = await _context.EnrolledCoursesDTO.FromSqlRaw("CALL sp_getEnrolledCourses (@UserID)", user).ToListAsync();

            return Ok(getCourse);
        }

        [HttpPost("EnrolledCourses")]
        public async Task<ActionResult> EnrollCourse([FromBody] EnrolledCourses model)
        {
            var existing = await _context.EnrolledCourses.FirstOrDefaultAsync(e => e.UserId == model.UserId && e.CourseId == model.CourseId);

            if (existing != null)
            {
                existing.Status = model.Status;
                existing.Comments = model.Comments;

            }
            else
            {
                _context.EnrolledCourses.Add(model);

            }
            var data = await _context.SaveChangesAsync();
            if (data > 0)
            {
                return Ok(model);

            }
            else
            {
                return BadRequest("No changes were made to the database.");
            }

        }
        [HttpGet("CourseDetails")]
        public async Task<ActionResult> GetCourseDetails([FromQuery] int userId, int courseId)
        {
            var user = new MySqlParameter("@userIDParam", userId);
            var course = new MySqlParameter("@courseIDParam", courseId);

            var data = await _context.CoursesDetailsDTO.FromSqlRaw("CALL sp_getCourseDetails (@userIDParam, @courseIDParam)", user, course).ToListAsync();

            if (data.Count > 0)
            {
                return Ok(data);
            }
            else
            {
                return NotFound("No Courses Found");
            }
        }
    }
}
