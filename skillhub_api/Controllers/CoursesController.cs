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

        [HttpPut("EditCourse/{courseId}")]
        public async Task<ActionResult> EditCourse(int courseId, [FromBody] Courses course)
        {
            var existingData = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId);
            if (existingData == null)
            {
                return NotFound();
            }
           
            existingData.Title=course.Title;
            existingData.Description=course.Description;
            existingData.ArticleLink1 = course.ArticleLink1;
            existingData.ArticleLink2 = course.ArticleLink2;
            existingData.ArticleLink3 = course.ArticleLink3;
            existingData.VideoLink1 = course.VideoLink1;
            existingData.VideoLink2 = course.VideoLink2;
            existingData.VideoLink3 = course.VideoLink3;

            var updatedData = await _context.SaveChangesAsync();
            if (updatedData > 0)
            {
                return Ok(existingData);
            }
            else
            {
                return BadRequest("Failed to update Data");
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

        [HttpPost("ArchiveCourse/{courseid}")]
        public async Task<ActionResult> ArchiveCourse(int courseid)
        {
            var existing = await _context.Courses.FirstOrDefaultAsync(c=>c.CourseId==courseid);
            if (existing == null)
            {
                return NotFound("No such course found");
            }
           
            var id = new MySqlParameter("@CourseIdParam", courseid);
            var data = await _context.Database.ExecuteSqlRawAsync("CALL sp_ArchiveCourse (@CourseIdParam)", id);

            if (data > 0)
            {
                return Ok("Course Archived Successfully");
            }
            else
            {
                return BadRequest("Failed to Archive Course");
            }

        }

    }
}
