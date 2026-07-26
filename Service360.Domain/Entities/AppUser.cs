using Microsoft.AspNetCore.Identity;

namespace Service360.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public Guid CompanyId { get; set; }

        public Company Company { get; set; } = null!;
    }
}
