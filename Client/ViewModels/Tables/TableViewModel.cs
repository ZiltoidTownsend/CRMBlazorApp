using Client.Managers;

namespace Client.ViewModels.Tables;

public class TableViewModel
{
    public List<TableHeaderItemData> Headers { get; set; }
    public List<TableRowViewModel> Rows { get; set; }
}
