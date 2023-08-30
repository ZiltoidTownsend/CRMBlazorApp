using Client.ViewModels.Tables;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.ObjectModel;

namespace Client.Pages.Entities.Reestrs;
partial class BaseReestr<TEntity>
{
    public TableViewModel? TableViewModel { get; set; }
    public List<TEntity> Items = new List<TEntity>();
    [Parameter]
    public RenderFragment HeaderEntity { get; set; }
    private List<string> GetHeaders()
    {
        var headers = new List<string>();
        var entityType = typeof(TEntity);

        var properties = entityType.GetProperties();

        foreach (var prop in properties)
        {
            headers.Add(prop.Name);
        }

        return headers;
    }
    protected override void OnInitialized()
    {
        base.OnInitialized();

        TableViewModel = new TableViewModel();
    }
}
