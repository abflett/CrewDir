using CrewDir.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace CrewDir.Api.Data.Identity
{
    public class AppUser : IdentityUser
    {
        public Employee? Employee { get; set; }
    }
}
