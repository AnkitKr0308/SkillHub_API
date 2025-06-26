using System.ComponentModel.DataAnnotations;

namespace skillhub_api.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }

        public string Role => RoleId switch
        {
            1 => "Admin",
            2 => "Mentor",
            3 => "Learner"
        };

    }
}
