using Avalonia.Input;

namespace UITests_E2E.E2E_Adapters;

public class XPathLocator : ILocator
{
	private readonly IInteractable root;
	private readonly string xPath;

	internal XPathLocator(IInteractable root, string xPath)
	{
		this.root = root;
		this.xPath = xPath;
	}

	InputElement ILocator.Find()
	{
		var element = ApplicationMap.FindByXPath(root?.GetTarget(), xPath);
		return element as InputElement;
	}
}