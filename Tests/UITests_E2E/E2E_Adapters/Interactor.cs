using System.Reflection;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Raw;
using Avalonia.Rendering;
using Avalonia.VisualTree;
using Eremex.AvaloniaUI.Controls.Utils;

namespace UITests_E2E.E2E_Adapters;

public static class Interactor
{
	public static Task SendRightMouseDownAsync(this IInteractable interactable, Point? point = default)
	{
		return interactable.SendMouseEventAsync(RawPointerEventType.RightButtonDown, point, RawInputModifiers.None);
	}

	public static Task SendRightMouseUpAsync(this IInteractable interactable, Point? point = default)
	{
		return interactable.SendMouseEventAsync(RawPointerEventType.RightButtonUp, point, RawInputModifiers.None);
	}

	public static Task SendLeftMouseDownAsync(this IInteractable interactable, Point? point = default, RawInputModifiers modifiers = default)
	{
		return interactable.SendMouseEventAsync(RawPointerEventType.LeftButtonDown, point, modifiers);
	}

	public static Task SendLeftMouseUpAsync(this IInteractable interactable, Point? point = default, RawInputModifiers modifiers = default)
	{
		return interactable.SendMouseEventAsync(RawPointerEventType.LeftButtonUp, point, modifiers);
	}

	public static async Task ClickAsync(this IInteractable interactable, Point? point = default)
	{
		await interactable.SendLeftMouseDownAsync(point);
		await interactable.SendLeftMouseUpAsync(point);
	}

	public static async Task DoubleClickAsync(this IInteractable interactable, Point? point = default)
	{
		await interactable.ClickAsync(point);
		await interactable.ClickAsync(point);
	}

	public static async Task RightClickAsync(this IInteractable interactable, Point? point = default)
	{
		await interactable.SendRightMouseDownAsync(point);
		await interactable.SendRightMouseUpAsync(point);
	}

	public static Task SendMouseMoveAsync(this IInteractable interactable, Point? point = default, RawInputModifiers modifiers = default)
	{
		return interactable.SendMouseEventAsync(RawPointerEventType.Move, point, modifiers);
	}

	public static Task SendKeyDownAsync(this IInteractable interactable, Key key, KeyModifiers modifiers = default)
	{
		return interactable.SendKeyboardEventAsync(RawKeyEventType.KeyDown, key, ToRawInputModifiers(modifiers));
	}

	public static async Task SendKeyPressAsync(this IInteractable interactable, Key key, KeyModifiers modifiers = default)
	{
		await interactable.SendKeyDownAsync(key, modifiers);
		await interactable.SendKeyUpAsync(key, modifiers);
	}

	public static Task SendKeyUpAsync(this IInteractable interactable, Key key, KeyModifiers modifiers = default)
	{
		return interactable.SendKeyboardEventAsync(RawKeyEventType.KeyUp, key, ToRawInputModifiers(modifiers));
	}

	public static Task SendTextInputAsync(this IInteractable interactable, string inputText)
	{
		return interactable.SendTextInputEventAsync(inputText);
	}

	private static Task SendTextInputEventAsync(this IInteractable interactable, string text)
	{
		return interactable.DoActionAndWaitAsync(() =>
		{
			var target = interactable.GetTarget();
			var visualRoot = target.GetVisualRoot();
			var e = V11TestUtils.CreateRawTextInputEventArgs((IInputRoot)visualRoot, text);
			HandleInput(e);
		});
	}

	private static Task SendKeyboardEventAsync(this IInteractable interactable, RawKeyEventType eventType, Key key, RawInputModifiers inputModifiers)
	{
		return interactable.DoActionAndWaitAsync(() =>
		{
			var target = interactable.GetTarget();
			var visualRoot = target.GetVisualRoot();
			var e = V11TestUtils.CreateRawKeyEventArgs((IInputRoot)visualRoot, eventType, key, inputModifiers);
			HandleInput(e);
		});
	}

	private static Task SendMouseEventAsync(this IInteractable interactable, RawPointerEventType eventType, Point? point, RawInputModifiers modifiers)
	{
		return interactable.DoActionAndWaitAsync(() =>
		{
			var target = interactable.GetTarget();
			var visualRoot = target.GetVisualRoot();
			var clientPoint = GetClientPoint(visualRoot, target, point);
			var e = V11TestUtils.CreateRawPointerEventArgs((IInputRoot)visualRoot, eventType, clientPoint, modifiers);
			HandleInput(e);
		});
	}

	private static RawInputModifiers ToRawInputModifiers(KeyModifiers modifiers)
	{
		return modifiers switch
		{
			KeyModifiers.Control => RawInputModifiers.Control,
			KeyModifiers.Shift => RawInputModifiers.Shift,
			_ => RawInputModifiers.None
		};
	}

	private static void HandleInput(RawInputEventArgs args)
	{
		var root = (TopLevel)args.GetRoot();
		root.SetTestInput(args);
		HandleInputMethod.Invoke(root, [ args ]);
		root.SetTestInput(null);
	}
	public static string GetLogMessage(this RawInputEventArgs args, string methodName)
	{
		var parameters = args switch
		{
			RawPointerEventArgs pointerArgs => $", {pointerArgs.GetEventType()}, ({pointerArgs.GetPosition()})",
			_ => null
		};
		return $"{methodName}({args.GetType().Name}): {args.GetRoot().GetType().Name}{parameters}";
	}
	private static readonly AttachedProperty<RawInputEventArgs> TestInputProperty = AvaloniaProperty.RegisterAttached<IInteractable, TopLevel, RawInputEventArgs>("TestInput");
	public static RawInputEventArgs GetTestInput(this TopLevel root) => root.GetValue(TestInputProperty);
	private static void SetTestInput(this TopLevel root, RawInputEventArgs args) => root.SetValue(TestInputProperty, args);
	private static readonly MethodInfo HandleInputMethod = typeof(TopLevel).GetMethod("HandleInput", BindingFlags.Instance | BindingFlags.NonPublic);

	private static Point GetClientPoint(IRenderRoot root, InputElement inputElement, Point? point)
	{
		var inputPoint = point ?? new Point(inputElement.Bounds.Width / 2, inputElement.Bounds.Height / 2);
		return inputElement.TranslatePoint(inputPoint, (Visual)root).Value;
	}
}