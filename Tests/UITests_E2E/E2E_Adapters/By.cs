using Avalonia.Input;

namespace UITests_E2E.E2E_Adapters;

public static class By
{
	public static ILocator WindowLocator<T>() where T : InputElement
	{
		return new WindowLocator<T>();
	}

	public static ILocator InputElementLocator<T>(Func<T, bool> handler = null) where T : InputElement
	{
		return InputElementLocator(null, handler);
	}
	public static ILocator InputElementLocator<T>(InputElementAdapter root, Func<T, bool> handler = null) where T : InputElement
	{
		return new ByTypeLocator<T>(root, handler ?? (_ => true));
	}

	public static ILocator XPathLocator(string xPath)
	{
		return XPathLocator(null, xPath);
	}
	public static ILocator XPathLocator(InputElementAdapter root, string xPath)
	{
		return new XPathLocator(root, xPath);
	}
}