using Domain.Entities;

namespace Client.Managers;

public class ContactManager : BaseManager<Contact>
{
    public ContactManager(HttpClient httpClient) : base(httpClient)
    {
    }
    public override Task<List<Contact>> GetAllEntitiesAsync() => base.GetAllEntitiesAsync();
    protected override string EntityPath => "contacts";
}
