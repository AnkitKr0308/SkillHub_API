using System.ComponentModel.DataAnnotations;

namespace skillhub_api.Models
{
    public class EnrolledCourses
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int UserId { get; set; }
        public string? Comments { get; set; }
        public string? Status { get; set; }
    }
}
