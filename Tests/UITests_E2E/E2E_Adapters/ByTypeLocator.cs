using Avalonia;
using Avalonia.Input;

namespace UITests_E2E.E2E_Adapters;

public class ByTypeLocator<T> : ILocator where T : InputElement
{
	private IInteractable root;
	private readonly Func<T, bool> handler;

	internal ByTypeLocator(IInteractable root, Func<T, bool> handler)
	{
		this.root = root;
		this.handler = handler;
	}

	InputElement ILocator.Find()
	{
		var target = root?.GetTarget();
		if (target != null)
		{
			return Find(target);
		}
		else
		{
			foreach (var currentRoot in ApplicationMap.GetRootItems())
			{
				if (Find(currentRoot) is { } result)
				{
					return result;
				}
			}
		}
		return null;
	}
	private InputElement Find(Visual root)
	{
		return ApplicationMap.GetDescendants(root).OfType<T>().FirstOrDefault(handler);
	}
}