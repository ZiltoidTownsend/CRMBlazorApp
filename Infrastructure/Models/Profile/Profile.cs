using Domain.Contracts;
using Infrastructure.Models.Identity;

namespace Infrastructure.Models.Profile;

public class Profile : AuditableEntity<Guid>
{
    public CRMUser CRMUser { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
}
