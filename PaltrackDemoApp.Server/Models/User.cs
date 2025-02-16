using System.ComponentModel.DataAnnotations;

namespace PaltrackDemoApp.Server.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
