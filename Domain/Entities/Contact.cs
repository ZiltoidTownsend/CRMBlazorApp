using Domain.Contracts;

namespace Domain.Entities;

public class Contact : AuditableEntity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string FullName { get; set; }
}
