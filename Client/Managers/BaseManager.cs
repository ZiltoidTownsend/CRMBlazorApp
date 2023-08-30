using Domain.Contracts;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Client.Managers;

public abstract class BaseManager<TEntity> where TEntity : AuditableEntity<Guid>
{
    protected HttpClient HttpClient { get; set; }
    protected virtual string EntityPath { get; set; }
    public BaseManager(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public async Task<List<TEntity>> GetAllEntitiesAsync()
    {
        var response = await HttpClient.GetAsync(EntityPath);
        var responseAsString = await response.Content.ReadAsStringAsync();
        var responseEntities = JsonSerializer.Deserialize<List<TEntity>>(responseAsString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve
        });

        return responseEntities.ToList();
    }
}
