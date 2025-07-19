using Avalonia.Input;

namespace UITests_E2E.E2E_Adapters;

public class WindowLocator<T> : ILocator where T : InputElement
{
	InputElement ILocator.Find()
	{
		return ApplicationMap.GetRootItems().OfType<T>().FirstOrDefault(x => x.GetType() == typeof(T));
	}
}