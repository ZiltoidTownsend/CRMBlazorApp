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

        Items = await manager.GetAllEntitiesAsync();

        CreateTableViewModel();
    }
    private void CreateTableViewModel()
    {
        TableViewModel = new TableViewModel();
        
        TableViewModel.Rows = new List<TableRowViewModel>();

        TableViewModel.Headers = GetTableHeaderData();

        foreach (var item in Items)
        {
            TableViewModel.Rows.Add(CreateRow(item));
        }
    }
    private List<TableHeaderItemData> GetTableHeaderData()
    {
        var dataFromProfile = _profileManager.GetProfileDataByKey("asd");

        return dataFromProfile.Count == 0 ? _defaultHeaderData : dataFromProfile;      
    }
    private List<TableHeaderItemData> _defaultHeaderData => new List<TableHeaderItemData>
    {
        new TableHeaderItemData
        {
            Value = "FirstName",
            DisplayValue = "Имя",
            Position = 1,
            Weight = 1,
        },
        new TableHeaderItemData
        {
            Value = "LastName",
            DisplayValue = "Фамилия",
            Position = 2,
            Weight = 1,
        },
    };
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
