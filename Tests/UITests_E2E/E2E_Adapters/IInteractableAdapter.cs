using Avalonia.Input;

namespace UITests_E2E.E2E_Adapters;

public interface IInteractable : IWaitable
{
	InputElement GetTarget();
}