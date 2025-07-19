using System.ComponentModel.DataAnnotations;

namespace skillhub_api.Models
    
{
    public class Creds
    {
        [Key]
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}
