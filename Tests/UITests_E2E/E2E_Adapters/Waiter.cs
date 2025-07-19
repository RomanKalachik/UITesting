using Avalonia.Input;
using Avalonia.Threading;

namespace UITests_E2E.E2E_Adapters;
public static class Waiter
{
	public static async Task<T> TryWaitUntilLoadedAsync<T>(this IWaitable waitable, TimeSpan timeout = default) where T : class, IWaitable
	{
		return await waitable.TryWaitUntilAsync<T>(GetIsLoadedAsync, timeout);
	}
	public static Task WaitUntilLoadedAsync(this IWaitable waitable, TimeSpan timeout = default, string message = null)
	{
		return waitable.WaitUntilLoadedAsync<InputElementAdapter>(timeout, message);
	}
	public static async Task<T> WaitUntilLoadedAsync<T>(this IWaitable waitable, TimeSpan timeout = default, string message = null) where T : class, IWaitable
	{
		return await waitable.WaitUntilAsync<T>(GetIsLoadedAsync, timeout, message);
	}
	private static Task<bool> GetIsLoadedAsync<T>(T waitable) where T : class, IWaitable
	{
		return waitable is InputElementAdapter inputElementAdapter ? inputElementAdapter.GetIsLoadedAsync() : Task.FromResult(false);
	}

	public static Task WaitUntilUnloadedAsync(this IWaitable waitable, TimeSpan timeout = default, string message = null)
	{
		return waitable.WaitUntilUnloadedAsync<InputElementAdapter>(timeout, message);
	}
	public static async Task<T> WaitUntilUnloadedAsync<T>(this IWaitable waitable, TimeSpan timeout = default, string message = null) where T : class, IWaitable
	{
		return await waitable.WaitUntilAsync<T>(GetIsUnloadedAsync, timeout, message);
	}
	private static async Task<bool> GetIsUnloadedAsync<T>(T waitable) where T : class, IWaitable
	{
		return !await GetIsLoadedAsync(waitable);
	}

	public static async Task<bool> TryWaitUntilVisibleAsync(this IWaitable waitable, TimeSpan timeout = default)
	{
		return await waitable.TryWaitUntilVisibleAsync<InputElementAdapter>(timeout) != null;
	}
	public static async Task<T> TryWaitUntilVisibleAsync<T>(this IWaitable waitable, TimeSpan timeout = default) where T : class, IWaitable
	{
		return await waitable.TryWaitUntilAsync<T>(GetIsVisibleAsync, timeout);
	}
	public static Task WaitUntilVisibleAsync(this IWaitable waitable, TimeSpan timeout = default, string message = null)
	{
		return waitable.WaitUntilVisibleAsync<InputElementAdapter>(timeout, message);
	}
	public static async Task<T> WaitUntilVisibleAsync<T>(this IWaitable waitable, TimeSpan timeout = default, string message = null) where T : class, IWaitable
	{
		return await waitable.WaitUntilAsync<T>(GetIsVisibleAsync, timeout, message);
	}
	private static Task<bool> GetIsVisibleAsync<T>(T waitable) where T : class, IWaitable
	{
		return waitable is InputElementAdapter inputElementAdapter ? inputElementAdapter.GetIsVisibleAsync() : Task.FromResult(false);
	}

	public static async Task WaitUntilInViewportAsync(this IWaitable waitable, TimeSpan timeout = default, string message = null)
	{
		await waitable.WaitUntilInViewportAsync<InputElementAdapter>(timeout, message);
	}
	public static async Task<T> WaitUntilInViewportAsync<T>(this IWaitable waitable, TimeSpan timeout = default, string message = null) where T : class, IWaitable
	{
		return await waitable.WaitUntilAsync<T>(GetIsInViewportAsync, timeout, message);
	}
	private static Task<bool> GetIsInViewportAsync<T>(T waitable) where T : class, IWaitable
	{
		return waitable is InputElementAdapter inputElementAdapter ? inputElementAdapter.GetIsInViewportAsync() : Task.FromResult(false);
	}

	public static async Task<bool> TryWaitUntilEnabledAsync(this IWaitable waitable, TimeSpan timeout = default)
	{
		return await waitable.TryWaitUntilAsync<InputElementAdapter>(GetIsEnabledAsync, timeout) != null;
	}
	public static Task WaitUntilEnabledAsync(this IWaitable waitable, TimeSpan timeout = default, string message = null)
	{
		return waitable.WaitUntilEnabledAsync<InputElementAdapter>(timeout, message);
	}
	public static async Task<T> WaitUntilEnabledAsync<T>(this IWaitable waitable, TimeSpan timeout = default, string message = null) where T : class, IWaitable
	{
		return await waitable.WaitUntilAsync<T>(GetIsEnabledAsync, timeout, message);
	}
	private static Task<bool> GetIsEnabledAsync<T>(T waitable) where T : class, IWaitable
	{
		return waitable is InputElementAdapter inputElementAdapter ? inputElementAdapter.GetIsEnabledAsync() : Task.FromResult(false);
	}

	public static Task WaitUntilEffectivelyEnabledAsync(this IWaitable waitable, TimeSpan timeout = default, string message = null)
	{
		return waitable.WaitUntilEffectivelyEnabledAsync<InputElementAdapter>(timeout, message);
	}
	public static async Task<T> WaitUntilEffectivelyEnabledAsync<T>(this IWaitable waitable, TimeSpan timeout = default, string message = null) where T : class, IWaitable
	{
		return await waitable.WaitUntilAsync<T>(GetIsEffectivelyEnabledAsync, timeout, message);
	}
	private static Task<bool> GetIsEffectivelyEnabledAsync<T>(T waitable) where T : class, IWaitable
	{
		return waitable is InputElementAdapter inputElementAdapter ? inputElementAdapter.GetIsEffectivelyEnabledAsync() : Task.FromResult(false);
	}

	public static Task WaitUntilDisabledAsync(this IWaitable waitable, TimeSpan timeout = default, string message = null)
	{
		return waitable.WaitUntilDisabledAsync<InputElementAdapter>(timeout, message);
	}
	public static async Task<T> WaitUntilDisabledAsync<T>(this IWaitable waitable, TimeSpan timeout = default, string message = null) where T : class, IWaitable
	{
		return await waitable.WaitUntilAsync<T>(GetIsDisabledAsync, timeout, message);
	}
	private static async Task<bool> GetIsDisabledAsync<T>(T waitable) where T : class, IWaitable
	{
		return waitable is InputElementAdapter inputElementAdapter ? !await inputElementAdapter.GetIsEnabledAsync() : false;
	}

	public static Task WaitUntilFocusedAsync(this IWaitable waitable, TimeSpan timeout = default, string message = null)
	{
		return waitable.WaitUntilFocusedAsync<InputElementAdapter>(timeout, message);
	}
	public static async Task<T> WaitUntilFocusedAsync<T>(this IWaitable waitable, TimeSpan timeout = default, string message = null) where T : class, IWaitable
	{
		return await waitable.WaitUntilAsync<T>(GetIsFocusedAsync, timeout, message);
	}
	private static Task<bool> GetIsFocusedAsync<T>(T waitable) where T : class, IWaitable
	{
		return waitable is InputElementAdapter inputElementAdapter ? inputElementAdapter.GetIsFocusedAsync() : Task.FromResult(false);
	}

	public static async Task<bool> TryWaitUntilAsync<T>(this T waitable, Func<bool> handler, TimeSpan timeout = default) where T : class, IWaitable
	{
		return await waitable.TryWaitUntilAsync(_ => waitable, _ => Task.FromResult(handler()), timeout) != null;
	}
	public static async Task WaitUntilAsync<T>(this T waitable, Func<bool> handler, TimeSpan timeout = default, string message = null) where T : class, IWaitable
	{
		await waitable.WaitUntilAsync(_ => waitable, _ => Task.FromResult(handler()), timeout, message);
	}

	private static async Task<T> TryWaitUntilAsync<T>(this IWaitable waitable, Func<T, Task<bool>> predicateAsync, TimeSpan timeout) where T : class, IWaitable
	{
		return await waitable.TryWaitUntilAsync(w => GetAdapter<T>(w), predicateAsync, timeout);
	}
	private static async Task<T> WaitUntilAsync<T>(this IWaitable waitable, Func<T, Task<bool>> predicateAsync, TimeSpan timeout, string message) where T : class, IWaitable
	{
		return await waitable.WaitUntilAsync(w => GetAdapter<T>(w), predicateAsync, timeout, message);
	}
	private static async Task<T> TryWaitUntilAsync<T>(this IWaitable waitable, Func<IWaitable, T> getAdapter, Func<T, Task<bool>> predicateAsync, TimeSpan timeout) where T : class, IWaitable
	{
		return await WaitUntilAsync(waitable, getAdapter, predicateAsync, safe: true, timeout);
	}
	private static async Task<T> WaitUntilAsync<T>(this IWaitable waitable, Func<IWaitable, T> getAdapter, Func<T, Task<bool>> predicateAsync, TimeSpan timeout, string message) where T : class, IWaitable
	{
		try
		{
			return await WaitUntilAsync(waitable, getAdapter, predicateAsync, safe: false, timeout);
		}
		catch (OperationCanceledException)
		{
			if (message != null)
				Assert.Fail(message);
			throw;
		}
	}
	private static async Task<T> WaitUntilAsync<T>(IWaitable waitable, Func<IWaitable, T> getAdapter, Func<T, Task<bool>> predicateAsync, bool safe, TimeSpan timeout) where T : class, IWaitable
	{
		if (timeout == default)
		{
			timeout = TimeSpan.FromSeconds(5);
		}
		using var timeoutCancellationTokenSource = new CancellationTokenSource(timeout);
		return await WaitUntilAsync(waitable, getAdapter, predicateAsync, safe, timeoutCancellationTokenSource.Token);
	}
	private static async Task<T> WaitUntilAsync<T>(IWaitable waitable, Func<IWaitable, T> getAdapter, Func<T, Task<bool>> predicateAsync, bool safe, CancellationToken cancellationToken) where T : class, IWaitable
	{
		while (true)
		{
			var readyAdapter = await InvokeOnUIThreadAsync(async () =>
			{
				var adapter = getAdapter(waitable);
				return adapter != null && await predicateAsync(adapter) ? adapter : null;
			});

			if (readyAdapter != null)
			{
				return readyAdapter;
			}

			if (cancellationToken.IsCancellationRequested)
			{
				if (safe)
				{
					return null;
				}
				else
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
			}

			await Task.Delay(TimeSpan.FromSeconds(2), CancellationToken.None);
		}
	}
	private static T GetAdapter<T>(IWaitable waitable) where T : class, IWaitable
	{
		var element = GetElement(waitable);
		return element != null ? (T)Activator.CreateInstance(typeof(T), element) : default;
	}
	private static InputElement GetElement(IWaitable waitable)
	{
		return waitable switch
		{
			ILocator locator => locator.Find(),
			IInteractable interactable => interactable.GetTarget(),
			_ => throw new ArgumentException("unknown")
		};
	}

	private static Task<T> InvokeOnUIThreadAsync<T>(Func<Task<T>> actionAsync)
	{
		if (Dispatcher.UIThread.CheckAccess())
		{
			return actionAsync();
		}

		return Dispatcher.UIThread.InvokeAsync(actionAsync);
	}

	public static async Task<T> DoActionAndWaitAsync<T>(this IWaitable waitable, Func<T> action)
	{
		var t = await Dispatcher.UIThread.InvokeAsync(action);
		await waitable.PushFrameAsync();
		return t;
	}
	public static async Task DoActionAndWaitAsync(this IWaitable waitable, Func<Task> actionAsync)
	{
		await Dispatcher.UIThread.InvokeAsync(actionAsync);
		await waitable.PushFrameAsync();
	}
	public static async Task DoActionAndWaitAsync(this IWaitable waitable, Action action)
	{
		await Dispatcher.UIThread.InvokeAsync(action);
		await waitable.PushFrameAsync();
	}
	public static async Task DoActionAsync(this IWaitable waitable, Action action)
	{
		var task = Dispatcher.UIThread.InvokeAsync(action).GetTask();
		await waitable.PushFrameAsync();

		var awaiter = task.GetAwaiter();
		if (awaiter.IsCompleted)
			awaiter.GetResult();
		else
			AmbientContext.Get<List<Task>>(nameof(Waiter)).Add(task);
	}
	public static async Task PushFrameAsync(this IWaitable _)
	{
		await Task.Delay(TimeSpan.FromSeconds(2));
		await Dispatcher.UIThread.InvokeAsync(() => { }, DispatcherPriority.ContextIdle);
	}

	public static void InitializePendingTasksAmbientContext() => AmbientContext.Set(nameof(Waiter), new List<Task>());
	public static Task WaitUntilPendingTasksCompletedAsync() => Task.WhenAll(AmbientContext.Get<List<Task>>(nameof(Waiter)));
}