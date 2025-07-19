using Avalonia.Controls;


namespace UITests_E2E.E2E_Adapters;

public class LabelAdapter : InputElementAdapter
{
	private Label Label => (Label)Element;

	public LabelAdapter(Label element) : base(element) { }

	public Task WaitUntilTextPresentAsync(string text, TimeSpan timeout = default, string message = null)
	{
		return this.WaitUntilAsync(() => string.Equals(Label.Content, text), timeout, message);
	}
}