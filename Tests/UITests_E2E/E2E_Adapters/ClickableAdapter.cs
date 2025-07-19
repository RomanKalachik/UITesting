using Avalonia.Input;

using Eremex.AvaloniaUI.Controls.Bars;
using Eremex.AvaloniaUI.Controls.Editors;
using ReflectionMagic;


namespace UITests_E2E.E2E_Adapters;

public class ClickableAdapter : InputElementAdapter
{
	public ClickableAdapter(InputElement element) : base(element) { }

	public async Task ClickAsync()
	{
		await this.WaitUntilLoadedAsync();
		await this.DoActionAsync(() =>
		{
			if (this.Element is ToolbarItem ti)
			{
				ti.AsDynamic().OnClick(EventArgs.Empty);
			}
			else if (this.Element is CheckEditor ce)
			{
				
					ce.IsChecked = !ce.IsChecked;
			}
			else
			{
				Element.AsDynamic().OnClick();
			}
		});
	}
}