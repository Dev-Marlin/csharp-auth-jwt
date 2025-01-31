using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.ViewModels
{
    public class UserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
