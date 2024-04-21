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
    private PageHistoryNavigationManager? _navigationManager { get; set; }
    [Inject]
    private ProfileManager? _profileManager { get; set; }
    private List<DropItem>? _items;
    private TableHeaderItemData[]? _headersData;
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _headersData = _profileManager?.GetProfileDataByKey(Key).ToArray();

        _items = new List<DropItem>();

        for(int i = 0; i < _headersData.Count(); i++)
        {
            _items.Add(new DropItem() { DisplayValue = _headersData[i].DisplayValue, Position = _headersData[i].Position, Weight = _headersData[i].Weight });
        }

        StateHasChanged();

        /*_tableViewModel.Headers = new List<string> { "Test", "Test2", "test3" };*/

    }
    private void NavigateToBack()
    {
        _navigationManager.NavigateBack();
    }

    void OnFocusHandler(string index, bool isSelect)
    {
        _items.First(i => i.Position == index).IsSelected = isSelect;
        StateHasChanged();
    }
    string GetClassForItem(string index) =>
        new CssBuilder()
        .AddClass("visible", _items.First(i => i.Position == index).IsSelected)
        .AddClass("invisible", !_items.First(i => i.Position == index).IsSelected)
        .Build();
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
        var item = _items.FirstOrDefault(i => i.Position == index.ToString());
        var cssClass = item != null ? $"mud-grid-item-xs-{item.Weight}" : "mud-grid-item-xs-1";

        return new CssBuilder(cssClass)
            .AddClass("border")
            .Build();
    }
    private void OnClickHandler(string index, int value)
    {
        var sumWeight = _items.Sum(i => i.Weight);

        var intIndex = int.Parse(index);

        if (sumWeight + value <= 12 && _items[intIndex].Weight + value != 0)
        {

            _items.FirstOrDefault(i => i.Position == index)!.Weight += value;
        }

        StateHasChanged();
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