using System.ComponentModel.DataAnnotations;

namespace skillhub_api.Models
{
    public class Courses
    {
        [Key]
        public int CourseId { get; set; }
        [Required]
        public string Title { get;   set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ArticleLink1 { get; set; }
        public string? ArticleLink2 { get; set; }
        public string? ArticleLink3 { get; set; }
        [Required]
        public string VideoLink1 { get; set; }
        public string? VideoLink2 { get; set; }
        public string? VideoLink3 { get; set; }
    }
}
