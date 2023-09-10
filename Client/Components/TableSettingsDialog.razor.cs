using Client.ViewModels.Tables;
using Microsoft.AspNetCore.Components;

namespace Client.Components;

public partial class TableSettingsDialog
{
    [Parameter]
    public string? Key { get; set; }

    private TableViewModel? _tableViewModel;
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _tableViewModel = new TableViewModel();

        /*_tableViewModel.Headers = new List<string> { "Test", "Test2", "test3" };*/

    }
}
