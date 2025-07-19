using System.ComponentModel.DataAnnotations;

namespace skillhub_api.DTO
{
    public class EnrolledCoursesDTO
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Comments { get; set; }
        public string Status { get; set; }
    }
}
