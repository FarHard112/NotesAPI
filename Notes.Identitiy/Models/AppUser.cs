using Microsoft.AspNetCore.Identity;

namespace Notes.Identitiy.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
