using Avalonia.Controls;

namespace UITests_E2E.E2E_Adapters;

public class WindowAdapter : InputElementAdapter
{
	private bool closed;
	private Window Window => (Window)Element;
	public WindowAdapter(Window window) : base(window) { }

	public async Task WaitUntilClosedAsync(TimeSpan timeout = default, string message = null)
	{
		await this.WaitUntilAsync(() => !Window.IsLoaded, timeout, message);
		await this.PushFrameAsync();
	}

	public async Task CloseAsync()
	{
		await this.DoActionAndWaitAsync(() =>
		{
			if (closed)
				return;
			closed = true;
			Window.Close();
		});
		await this.PushFrameAsync();
	}
}