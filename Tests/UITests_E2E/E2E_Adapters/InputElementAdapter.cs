using System.Runtime.CompilerServices;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.LogicalTree;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using Avalonia.VisualTree;
using Serilog;

namespace UITests_E2E.E2E_Adapters;

public class InputElementAdapter : IInteractable
{
	private readonly InputElement element;
	private bool isInitialized;

	public InputElementAdapter(InputElement element)
	{
		ArgumentNullException.ThrowIfNull(element);
		this.element = element;
	}

	protected InputElement Element
	{
		get
		{
			element.VerifyAccess();
			return element;
		}
	}

	InputElement IInteractable.GetTarget()
	{
		return element;
	}

	public async Task<bool> GetIsLoadedAsync()
	{
		return await InvokeOnUIThreadAsync(() => IsLoaded(Element));
	}
	public async Task<bool> GetIsVisibleAsync()
	{
		return await InvokeOnUIThreadAsync(() => Element.GetSelfAndVisualAncestors().All(e => IsLoaded(e) && e.IsVisible));
	}

	public async Task<bool> GetIsInViewportAsync()
	{
		return await InvokeOnUIThreadAsync(() =>
		{
			return Element.GetVisualAncestors().All(e =>
			{
				bool res = IsLoaded(e) && e.IsVisible &&
					new Rect(0, 0, e.Bounds.Width, e.Bounds.Height).Contains(Element.Bounds);
				return res;
			});
		});
	}
	public async Task<Rect> GetBoundsAsync()
	{
		return await InvokeOnUIThreadAsync(() => Control?.Bounds ?? new Rect());
	}
	public async Task<bool> GetIsEnabledAsync()
	{
		return await InvokeOnUIThreadAsync(() => Control?.IsEnabled ?? true);
	}
	public async Task<bool> GetIsEffectivelyEnabledAsync()
	{
		return await InvokeOnUIThreadAsync(() => Control?.IsEffectivelyEnabled ?? true);
	}
	public async Task<bool> GetIsFocusedAsync()
	{
		return await InvokeOnUIThreadAsync(() => Control?.IsKeyboardFocusWithin ?? true);
	}
	protected async Task<T> InvokeOnUIThreadAsync<T>(Func<T> function)
	{
		await EnsureInitializedAsync();
		if (Dispatcher.UIThread.CheckAccess())
		{
			return function();
		}
		return await Dispatcher.UIThread.InvokeAsync(function);
	}
	private async Task EnsureInitializedAsync()
	{
		if (isInitialized)
			return;
		await InitializedCore();
        isInitialized = true;
	}
	protected virtual Task InitializedCore()
	{
		return Task.CompletedTask;
	}
	private static bool IsLoaded(Visual visual)
	{
		return visual is Control control ? control.IsLoaded : true;
	}
	private Control Control => Element as Control;

	public async Task FocusAsync()
	{
		await this.WaitUntilLoadedAsync();

		await this.DoActionAndWaitAsync(() =>
		{
			Element.Focus();
		});

	}

	public Task DoActionAsync(Action<InputElement> handler)
	{
		return DoActionAsync<InputElement>(handler);
	}

	public Task<TV> GetAsync<TC, TV>(Func<TC, TV> handler) where TC : InputElement
	{
		return this.DoActionAndWaitAsync(() => handler((TC)Element));
	}

	public Task SetDataContextAsync(Func<object> handler)
	{
		return this.DoActionAndWaitAsync(() => Control.DataContext = handler());
	}

	public Task<TD> GetDataContextAsync<TD>()
	{
		return this.DoActionAndWaitAsync(() => (TD)Element.DataContext);
	}

	public Task<TV> GetFromDataContextAsync<TD, TV>(Func<TD, TV> handler)
	{
		return this.DoActionAndWaitAsync(() => handler((TD)Element.DataContext));
	}

	public async Task DoActionAsync<T>(Action<T> handler) where T : InputElement
	{
		await this.WaitUntilLoadedAsync();
		await this.DoActionAsync(() => handler((T)Element));
	}

	public async Task WaitUntilAsync<T>(Func<T, bool> handler, TimeSpan timeout = default, string message = null) where T : InputElement
	{
		await this.WaitUntilLoadedAsync(message: message);
		await this.WaitUntilAsync(() => handler((T)Element), timeout: timeout, message: message);
	}

	public Task<Bitmap> TakeScreenshotAsync()
	{
		return this.DoActionAndWaitAsync(() =>
		{
			var target = (InputElement)(Element.GetVisualRoot() ?? Element.FindLogicalAncestorOfType<TopLevel>());
			return target.RenderToBitmap();
		});
	}

	public async Task UpdateLayoutAsync()
	{
		var tcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
		await DoActionAsync(x =>
		{
			void OnLayoutUpdated(object sender, EventArgs e)
			{
				x.LayoutUpdated -= OnLayoutUpdated;
				tcs.SetResult();
			}

			x.LayoutUpdated += OnLayoutUpdated;
			x.UpdateLayout();
		});
		await tcs.Task;
	}

}