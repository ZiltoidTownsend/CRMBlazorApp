using Client.Managers;
using Client.ViewModels.Tables;
using Microsoft.AspNetCore.Components;
using MudBlazor.Utilities;
using static MudBlazor.CategoryTypes;

namespace Client.Pages.Settings;

public partial class TableSettingPage
{
    [Parameter]
    public string? Key { get; set; }
    [Inject]
    private PageHistoryNavigationManager _navigationManager { get; set; }
    [Inject]
    private ProfileManager _profileManager { get; set; }
    private DropItem[] _items;
    private TableHeaderItemData[] _headersData;
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _headersData = _profileManager.GetProfileDataByKey(Key).ToArray();

        _items = new DropItem[_headersData.Length];

        for(int i = 0; i < _headersData.Count(); i++)
        {
            _items[i] = new DropItem() { DisplayValue = _headersData[i].DisplayValue, Position = _headersData[i].Position };
        }

        StateHasChanged();

        /*_tableViewModel.Headers = new List<string> { "Test", "Test2", "test3" };*/

    }
    private void NavigateToBack()
    {
        _navigationManager.NavigateBack();
    }

    void OnFocusHandler(int index, bool isSelect)
    {
/*        _items[index - 1].IsSelected = isSelect;
        StateHasChanged();    */    
    }
    string GetClassForItem(int index) => "";
/*        new CssBuilder()
        .AddClass("visible", _items[index - 1].IsSelected)
        .AddClass("invisible", !_items[index - 1].IsSelected)
        .Build();*/
    string GetClassForItemContainer(int index)
    {
        var css = new CssBuilder()
            .AddClass($"mud-list-item-clickable")
            .AddClass("py-2")
            .AddClass("d-flex")
            .AddClass("flex-row");

        return css.Build();

    }

    string GetClassForDropZoneItem(int index)
    {
        var cssClass = _headersData.Count() > index? $"mud-grid-item-xs-{_headersData[index].Position}" : "mud-grid-item-xs-1";

        return new CssBuilder(cssClass).Build();
    }
}
public class DropItem
{
    public string? DisplayValue { get; set; }
    public string? Value { get; set; }
    public int Weight { get; set; }
    public string? Position { get; set; }
    public bool IsSelected { get; set; }
}