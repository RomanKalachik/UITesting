using Avalonia.Controls;
using Eremex.AvaloniaUI.Controls.Editors;

namespace UITests_E2E.E2E_Adapters;

public class TextEditorAdapter : BaseEditorAdapter
{
	public TextEditorAdapter(TextEditor element) : base(element)
	{
	}

	public async Task<string> GetEditText()
	{
		var textBox = await By.InputElementLocator<TextBox>(this).WaitUntilLoadedAsync<TextElementAdapter>();
		return await textBox.GetTextAsync();
	}
}