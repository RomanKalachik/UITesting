using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

using Eremex.AvaloniaUI.Controls.Editors;

namespace UITests_E2E.E2E_Adapters;

public class TextElementAdapter : InputElementAdapter
{
	public TextElementAdapter(InputElement element) : base(element) { }

	public async Task SelectAllAsync()
	{
		await this.WaitUntilLoadedAsync();
		await this.DoActionAndWaitAsync(() =>
		{
			var handler = GetSelectAllHandler();
			handler();
		});

	}

	public Task WaitUntilTextPresentAsync(string text, string message = null)
	{
		return this.WaitUntilAsync(() => GetText() == text, message: message);
	}

	public Task<string> GetTextAsync() =>
		this.DoActionAndWaitAsync(GetText);

	public Task<bool> GetIsReadOnlyAsync() =>
		this.DoActionAndWaitAsync(GetIsReadOnly);

	public async Task<string> WaitUntilNonEmptyTextPresentAsync(string message = null)
	{
		string text = string.Empty;
		await this.WaitUntilAsync(
			() =>
			{
				text = GetText();
				return !string.IsNullOrEmpty(text);
			},
			message: message);
		return text;
	}

	public async Task SendTextAsync(string text, bool selectAll = false, bool waitForTextPresent = true)
	{
		await FocusAsync();
		if (selectAll)
		{
			await SelectAllAsync();
			await this.SendKeyPressAsync(Key.Delete);
		}

		await this.SendTextInputAsync(text);
		if (waitForTextPresent)
		{
			await WaitUntilTextPresentAsync(text);
		}
	}

	private Action GetSelectAllHandler()
	{
		return Element switch
		{
			TextBox tb => tb.SelectAll,
			TextEditor te => te.SelectAll,
			_ => throw new NotSupportedException()
		};
	}

	private string GetText()
	{
		return Element switch
		{
			TextBlock tbl => tbl.Text,
			TextBox tb => tb.Text,
			TextEditor te => te.DisplayText,
			_ => throw new ArgumentException("not a text editor")
		};
	}

	private bool GetIsReadOnly()
	{
		return Element switch
		{
			TextBlock => true,
			TextBox tb => tb.IsReadOnly,
			TextEditor te => te.ReadOnly,
			_ => throw new ArgumentException("not a text editor")
		};
	}

	public async Task InputTextAsync(string text, bool selectAll = true)
	{
		await this.WaitUntilVisibleAsync();
		await FocusAsync();
		await this.WaitUntilFocusedAsync();
		if (selectAll)
			await SelectAllAsync();
		await this.SendTextInputAsync(text);
		await WaitUntilTextPresentAsync(text);
	}

	public async Task<bool> HasErrorsAsync()
	{
		return await this.DoActionAndWaitAsync(GetHasErrors);
	}

	private bool GetHasErrors()
	{
		return Element switch
		{
			TextBlock tbl => false,
			TextEditor te => te.ValidationInfo is not null,
			_ => throw new ArgumentException("not implemented")
		};
	}

	public Task<FontWeight> GetFontWeightAsync() => this.DoActionAndWaitAsync(GetFontWeight);

	private FontWeight GetFontWeight()
	{
		return Element switch
		{
			TextBlock t => t.FontWeight,
			TextBox tb => tb.FontWeight,
			TextEditor te => te.FontWeight,
			_ => throw new ArgumentException("not a text editor")
		};
	}
}