using Domain.Entities;

namespace Client.Managers;

public class ContactManager : BaseManager<Contact>
{
    public ContactManager(HttpClient httpClient) : base(httpClient)
    {
    }
    public override Task<List<Contact>> GetAllEntitiesAsync(int skipCount, int getCount, string sortingString)  => base.GetAllEntitiesAsync(skipCount, getCount, "");
    /*{
        List<Contact> contacts = new List<Contact>
        {
            new Contact
            {
                FirstName = "Егор",
                LastName = "Муравьев"
            },
            new Contact
            {
                FirstName = "Егор",
                LastName = "Муравьев"
            },
        };

        return Task.FromResult(contacts);
    }*/
    protected override string EntityPath => "Contacts";
}
