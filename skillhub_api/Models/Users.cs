using System.ComponentModel.DataAnnotations;

namespace skillhub_api.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int RoleId { get; set; }

        public string Role => RoleId switch
        {
            1 => "Admin",
            2 => "Mentor",
            3 => "Learner"
        };

    }
}
