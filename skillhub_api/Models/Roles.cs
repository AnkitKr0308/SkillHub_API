using System.ComponentModel.DataAnnotations;

namespace skillhub_api.Models
{
    public class Roles
    {
        [Key]
        public int ID { get; set; }
        public string Role { get; set; }
    }
}
