using Client.Managers;
using Client.Pages.Settings;
using Client.Services;
using Client.ViewModels.Tables;
using Domain.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Client.Pages.Entities.Reestrs;
partial class BaseReestr<TEntity> where TEntity : AuditableEntity<Guid>
{
    [Parameter]
    public string? TableKey { get; set; }
    [Inject]
    private ProfileManager? _profileManager { get; set; }
    [Inject]
    private IJSRuntime? js { get; set; }
    [Inject]
    private PageHistoryNavigationManager? _navigationManager { get; set; }
    private int selectedRowNumber = -1;
    private MudTable<TableRowViewModel> mudTable;
    private CustomScrollListener? CustomScrollListener;
    private int _entitiesCount = 0;
    private int _getCountEntities = 20;
    public TableViewModel? TableViewModel { get; set; }
    public List<TEntity> Items = new List<TEntity>();
    public string? ReestrTableKey { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        ReestrTableKey = $"Table";

        Items = await manager.GetAllEntitiesAsync(_entitiesCount, _getCountEntities, "");
        _entitiesCount += _getCountEntities;

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
            Position = "1",
            Weight = 1,
        },
        new TableHeaderItemData
        {
            Value = "LastName",
            DisplayValue = "Фамилия",
            Position = "2",
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
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {

            CustomScrollListener = new CustomScrollListener(null, js);

            //subscribe to event
            CustomScrollListener.OnCustomScroll += ScrollListener_OnScroll;
        }
    }

    private async void ScrollListener_OnScroll(object? sender, ScrollEventArgs e)
    {
        await AddItem();
        StateHasChanged();
    }
    private async Task AddItem()
    {
        Items = await manager.GetAllEntitiesAsync(_entitiesCount, _getCountEntities, "");
        _entitiesCount += _getCountEntities;

        foreach (var item in Items)
        {
            TableViewModel.Rows.Add(CreateRow(item));
        }
    }
    public void RedirectToSettingPage()
    {
        _navigationManager?.NavigateTo($"{nameof(TableSettingPage)}/{TableKey}");
        /*var parameters = new DialogParameters<TableSettingsDialog>();
        parameters.Add(x => x.Key, TableKey);

        var options = new DialogOptions() { CloseButton = true, FullScreen = true };

        Dialog.Show<TableSettingsDialog>("Настройки таблицы", parameters, options);*/
    }
    private void RowClickEvent(TableRowClickEventArgs<TableRowViewModel> tableRowClickEventArgs)
    {

    }
    private string SelectedRowClassFunc(TableRowViewModel element, int rowNumber)
    {
        if (selectedRowNumber == rowNumber)
        {
            selectedRowNumber = -1;
            return string.Empty;
        }
        else if (mudTable.SelectedItem != null && mudTable.SelectedItem.Equals(element))
        {
            selectedRowNumber = rowNumber;
            return "selected";
        }
        else
        {
            return string.Empty;
        }
    }
    private async Task<TableData<TableRowViewModel>> ServerReload(TableState state)
    {
        return null;
    }
}
