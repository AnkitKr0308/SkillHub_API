using System.ComponentModel.DataAnnotations;

namespace skillhub_api.DTO
{
    public class CourseDetailsDTO
    {
        [Key]
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ArticleLink1 { get; set; }
        public string ArticleLink2 { get; set; }
        public string ArticleLink3 { get; set; }
        public string VideoLink1 { get; set; }
        public string VideoLink2 { get; set; }
        public string VideoLink3 { get; set; }
        public int UserId { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
    }
}
