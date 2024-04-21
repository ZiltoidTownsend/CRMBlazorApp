namespace Infrastructure.Extensions;

public static class ObjectExtension
{
    public static T Get<T> (this object obj, string propertyName) where T : class
    {
        return (T)obj.GetType().GetProperty(propertyName).GetValue(obj, null);
    }
}
