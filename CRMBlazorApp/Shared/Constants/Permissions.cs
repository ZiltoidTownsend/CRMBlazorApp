using System.Reflection;

namespace CRMBlazorApp.Shared.Constants;
public static class Permissions
{
    public static class Contacts
    {
        public const string View = "Permissions.Contacts.View";
        public const string Create = "Permissions.Contacts.Create";
        public const string Edit = "Permissions.Contacts.Edit";
        public const string Delete = "Permissions.Contacts.Delete";
        public const string Export = "Permissions.Contacts.Export";
        public const string Search = "Permissions.Contacts.Search";
    }
    public static List<string> GetRegisteredPermissions()
    {
        var permssions = new List<string>();
        foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
        {
            var propertyValue = prop.GetValue(null);
            if (propertyValue is not null)
                permssions.Add(propertyValue.ToString());
        }
        return permssions;
    }
}
