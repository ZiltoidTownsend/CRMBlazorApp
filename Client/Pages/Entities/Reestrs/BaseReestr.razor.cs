using Client.Managers;
using Client.ViewModels.Tables;
using Domain.Contracts;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.Entities.Reestrs;
partial class BaseReestr<TEntity> where TEntity : AuditableEntity<Guid>
{
    [Inject]
    private ProfileManager? _profileManager { get; set; }
    public TableViewModel? TableViewModel { get; set; }
    public List<TEntity> Items = new List<TEntity>();
    public string? ReestrTableKey { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        ReestrTableKey = $"Table";

        //Items = await manager.GetAllEntitiesAsync();

        CreateTableViewModel();
    }
    private void CreateTableViewModel()
    {
        TableViewModel = new TableViewModel();
        TableViewModel.Headers = _profileManager.GetProfileDataByKey("asd");
        TableViewModel.Rows = new List<TableRowViewModel>();

        foreach (var item in Items)
        {
            TableViewModel.Rows.Add(CreateRow(item));
        }
    }
    private TableRowViewModel CreateRow(TEntity item)
    {
        var row = new TableRowViewModel();
        row.RowItems = new List<TableItemViewModel>();
        var entityType = typeof(TEntity);

        foreach(var tableItem in TableViewModel.Headers)
        {
            var property = entityType.GetProperty(tableItem.Value);

            row.RowItems.Add(new TableItemViewModel { IsLink = false, Value = property.GetValue(item).ToString() });
        }


        return row;
    }
}
