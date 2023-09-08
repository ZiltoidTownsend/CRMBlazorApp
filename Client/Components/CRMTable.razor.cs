using Client.Managers;
using Client.Pages.Settings;
using Client.ViewModels.Tables;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Components;

public partial class CRMTable
{
    [Parameter]
    [EditorRequired]
    public string? TableKey { get; set; }
    [Parameter]
    [EditorRequired]
    public TableViewModel TableViewModel { get; set; }
    [Inject]
    private PageHistoryNavigationManager _navigationManager { get; set; }
    [Inject]
    private ProfileManager _profileManager { get; set; }
    /*    [Inject]
        private IDialogService? Dialog { get; set; }*/
    protected override void OnInitialized()
    {
        base.OnInitialized();
    }
    public void RedirectToSettingPage()
    {
        _navigationManager?.NavigateTo($"{nameof(TableSettingPage)}/{TableKey}"); 
        /*var parameters = new DialogParameters<TableSettingsDialog>();
        parameters.Add(x => x.Key, TableKey);

        var options = new DialogOptions() { CloseButton = true, FullScreen = true };

        Dialog.Show<TableSettingsDialog>("Настройки таблицы", parameters, options);*/
    }
}
