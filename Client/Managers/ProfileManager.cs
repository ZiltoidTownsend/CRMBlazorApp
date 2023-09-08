namespace Client.Managers;

public class ProfileManager
{
    public List<TableHeaderItemData> GetProfileDataByKey(string key)
    {
        return new List<TableHeaderItemData> 
        {
            new TableHeaderItemData
            {
                DisplayValue = "Имя",
                Weight = 1,
                Position = 1
            },
            new TableHeaderItemData
            {
                DisplayValue = "Фамилия",
                Weight = 3,
                Position = 2
            },
        };
    }
}

public class TableHeaderItemData
{
    public string DisplayValue { get; set; }
    public string Value { get; set; }
    public int Weight { get; set; }
    public int Position { get; set; }
}