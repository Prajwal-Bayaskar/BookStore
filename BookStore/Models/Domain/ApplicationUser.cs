using Microsoft.AspNetCore.Identity;

namespace BookStore.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name{ get; set; }
    }
}
