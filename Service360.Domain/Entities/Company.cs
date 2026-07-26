namespace Service360.Domain.Entities
{
    public class Company : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;

        public string? TaxNumber { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<AppUser> Users { get; set; } = new List<AppUser>();
    }
}
