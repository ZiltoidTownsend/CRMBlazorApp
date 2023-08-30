using Microsoft.JSInterop;
using MudBlazor;

namespace Client.Services
{
    public class CustomScrollListener : IScrollListener
    {
        public CustomScrollListener(string selector, IJSRuntime js)
        {
            _js = js;
            Selector = selector;
        }
        public string Selector { get; set; } = null;

        private readonly IJSRuntime _js;
        private DotNetObjectReference<CustomScrollListener> _dotNetRef;

        public event EventHandler<ScrollEventArgs> OnScroll;
        public event EventHandler<ScrollEventArgs> OnCustomScroll
        {
            add => Subscribe(value);
            remove => Unsubscribe(value);
        }
        private EventHandler<ScrollEventArgs> _onScroll;

        [JSInvokable]
        public void CustomScroll(ScrollEventArgs e)
        {
            _onScroll?.Invoke(this, e);
        }

        private async void Subscribe(EventHandler<ScrollEventArgs> value)
        {
            if (_onScroll == null)
            {
                await Start();
            }
            _onScroll += value;
        }

        private void Unsubscribe(EventHandler<ScrollEventArgs> value)
        {
            _onScroll -= value;
            if (_onScroll == null)
            {
                Cancel().ConfigureAwait(false);
            }
        }
        private ValueTask<bool> Start()
        {
            _dotNetRef = DotNetObjectReference.Create(this);
            return _js.InvokeVoidAsyncWithErrorHandling
                ("customScrollListener.listenForScroll",
                           _dotNetRef,
                           Selector);
        }

        /// <summary>
        /// Unsubscribe to scroll event in 
        /// </summary>
        private async ValueTask Cancel()
        {
            try
            {
                await _js.InvokeVoidAsync(
                    "customScrollListener.cancelListener",
                               Selector);
            }
            catch { /* ignore */ }
        }

        public void Dispose()
        {
            _dotNetRef?.Dispose();
        }
    }
}
