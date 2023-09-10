using Client.Managers;
using Client.ViewModels.Tables;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.Settings;

public partial class TableSettingPage
{
    [Parameter]
    public string? Key { get; set; }
    [Inject]
    private PageHistoryNavigationManager _navigationManager { get; set; }
    [Inject]
    private ProfileManager _profileManager { get; set; }

    private List<TableHeaderItemData>? _headersData;
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _headersData = _profileManager.GetProfileDataByKey(Key);

        /*_tableViewModel.Headers = new List<string> { "Test", "Test2", "test3" };*/

    }
    private void NavigateToBack()
    {
        _navigationManager.NavigateBack();
    }
}
