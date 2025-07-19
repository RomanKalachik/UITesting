using Eremex.AvaloniaUI.Controls.Editors;

namespace UITests_E2E.E2E_Adapters;

public class BaseEditorAdapter : TextElementAdapter
{
	public BaseEditorAdapter(BaseEditor element) : base(element)
	{
	}

	public async Task<object> GetEditorValue()
	{
		return await GetAsync<BaseEditor, object>(x => x.EditorValue);
	}

	public async Task SetEditorValue(object value)
	{
		await DoActionAsync<BaseEditor>(x => x.EditorValue = value);
	}

	public async Task<T> GetEditorValue<T>()
	{
		return (T)await GetEditorValue();
	}
}