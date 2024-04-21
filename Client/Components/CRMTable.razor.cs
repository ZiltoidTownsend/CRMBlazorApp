using Client.Managers;
using Client.Pages.Settings;
using Client.ViewModels.Tables;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace Client.Components;

public partial class CRMTable
{
    [Parameter]
    [EditorRequired]
    public string? TableKey { get; set; }
    [Parameter]
    [EditorRequired]
    public TableViewModel? TableViewModel { get; set; }
    [Inject]
    private PageHistoryNavigationManager? _navigationManager { get; set; }
    [Inject]
    private ProfileManager? _profileManager { get; set; }
    private int selectedRowNumber = -1;
    private MudTable<TableRowViewModel> mudTable;
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
}
