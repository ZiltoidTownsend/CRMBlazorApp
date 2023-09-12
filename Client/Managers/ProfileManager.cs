namespace Client.Managers;

public class ProfileManager
{
    public List<TableHeaderItemData> GetProfileDataByKey(string key) => new List<TableHeaderItemData>
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

    private List<TableHeaderItemData> _tableData = new List<TableHeaderItemData>();
}
public class TableHeaderItemData
{
    public string? DisplayValue { get; set; }
    public string? Value { get; set; }
    public int Weight { get; set; }
    public string? Position { get; set; }
    public bool IsSelected { get; set; }
}

