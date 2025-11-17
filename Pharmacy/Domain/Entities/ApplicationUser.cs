using Microsoft.AspNetCore.Identity;

namespace Pharmacy.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
