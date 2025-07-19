using Avalonia.Controls;

namespace UITests_E2E.E2E_Adapters;

public class MainWindowAdapter : InputElementAdapter
{
	public MainWindowAdapter(Window element) : base(element) { }

	public async Task<WindowAdapter> ShowWindowAsync<T>() where T : Window, new()
	{
		await this.DoActionAndWaitAsync(() =>
		{
			var window = new T();
			window.Show();
		});
		var windowAdapter = By.WindowLocator<T>();
		return await windowAdapter.WaitUntilLoadedAsync<WindowAdapter>();
	}
}