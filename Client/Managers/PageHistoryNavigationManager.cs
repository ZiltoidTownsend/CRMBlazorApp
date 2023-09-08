using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Client.Managers;

public class PageHistoryNavigationManager : IDisposable
{
    private readonly NavigationManager _navigationManager;
    private readonly List<string> _history;
    public PageHistoryNavigationManager(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        _history = new List<string>();
        _history.Add(_navigationManager.Uri);
        _navigationManager.LocationChanged += OnLocationChanged;
    }
    public bool CanNavigateBack => _history.Count >= 2;
    public void NavigateTo(string url)
    {
        _navigationManager.NavigateTo(url);
    }
    public void NavigateBack()
    {
        if (!CanNavigateBack) return;
        var backPageUrl = _history[^2];
        _history.RemoveRange(_history.Count - 2, 2);
        _navigationManager.NavigateTo(backPageUrl);
    }
    private void OnLocationChanged(object sender, LocationChangedEventArgs e)
    {
        _history.Add(e.Location);
    }
    public void Dispose()
    {
        _navigationManager.LocationChanged -= OnLocationChanged;
    }
}
