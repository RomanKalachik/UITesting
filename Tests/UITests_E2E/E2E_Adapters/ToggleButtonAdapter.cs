using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.VisualTree;


using Eremex.AvaloniaUI.Controls.Bars;
using Eremex.AvaloniaUI.Controls.Editors;

namespace UITests_E2E.E2E_Adapters;

public class ToggleButtonAdapter : ClickableAdapter
{
	public ToggleButtonAdapter(InputElement element) : base(element)
	{
	}

	public async Task WaitUntilChecked(string message = null)
	{
		await this.WaitUntilAsync<InputElement>((x) =>
		{
			if (x is ToggleButton tbtn)
			{
				return tbtn.IsChecked == true;
			}
			if (x is CheckEditor ce)
			{
				return ce.IsChecked == true;
			}
			if (x is ToolbarCheckItem tbci)
			{
				return tbci.IsChecked;
			}
			return false;
		}, message: message);
		await this.PushFrameAsync();
	}

	public async Task WaitUntilUnchecked(string message = null)
	{
		await this.WaitUntilAsync<InputElement>((x) =>
		{
			if (x is ToggleButton tbtn)
			{
				return tbtn.IsChecked == false;
			}
			if (x is CheckEditor ce)
			{
				return ce.IsChecked == false;
			}
			if (x is ToolbarCheckItem tbci)
			{
				return !tbci.IsChecked;
			}

			return false;
		}, message: message);
		await this.PushFrameAsync();
	}

	public async Task<bool?> GetIsCheckedAsync()
	{
		var result = await this.GetAsync<InputElement, bool?>((x) =>
		{
			if (x is ToggleButton tbtn)
			{
				return tbtn.IsChecked;
			}
			if (x is CheckEditor ce)
			{
				return ce.IsChecked;
			}
			if (x is ToolbarCheckItem tbci)
			{
				return tbci.IsChecked;
			}
			return null;
		});
		await this.PushFrameAsync();
		return result;
	}

	public async Task Check()
	{
		while (await GetIsCheckedAsync() != true)
		{
			await this.ClickAsync();
		}
	}

	public async Task Uncheck()
	{
		while (await GetIsCheckedAsync() != false)
		{
			await this.ClickAsync();
		}
	}

}