using skillhub_api.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Utilities;
using skillhub_api.DTO;

namespace skillhub_api

{
    public class AppDbContext: DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { 
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Creds> Creds { get; set; } 
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<EnrolledCoursesDTO> EnrolledCoursesDTO { get; set; }
        public DbSet<EnrolledCourses> EnrolledCourses {  get; set; }
        public DbSet<CourseDetailsDTO> CoursesDetailsDTO { get;set; }
    }
}
