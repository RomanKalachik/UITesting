using System.Xml.Linq;
using System.Xml.XPath;

using Avalonia;
using Avalonia.Automation;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.VisualTree;
using Eremex.AvaloniaUI.Controls.Bars;
using Eremex.AvaloniaUI.Controls.Editors;
using Eremex.AvaloniaUI.Controls.PropertyGrid.Visuals;

namespace UITests_E2E.E2E_Adapters;

public static class ApplicationMap
{
	public static IEnumerable<TopLevel> GetRootItems()
	{
		var lifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
		return lifetime.Windows.Cast<TopLevel>();
	}
	public static IEnumerable<Visual> GetDescendants(Visual visual)
	{
		foreach (var child in GetChildren(visual))
		{
			yield return child;
			foreach (var descendant in GetDescendants(child))
			{
				yield return descendant;
			}
		}
	}
	public static Visual FindByXPath(Visual root, string xPath)
	{
		var rootNode = new XElement(root == null ? "Windows" : "Root");
		var items = root == null ? GetRootItems() : GetChildren(root);
		var id = 0;
		var visualById = new Dictionary<int, Visual>();
		foreach (var item in items)
		{
			AddNode(rootNode, item, ref id, visualById);
		}

		var element = rootNode.XPathSelectElement(xPath);
		return element == null ? null : visualById[(int)element.Attribute("Id")];
	}
	private static void AddNode(XContainer container, Visual visual, ref int id, Dictionary<int, Visual> visualById)
	{
		id++;
		var element = CreateNode(visual, id);
		container.Add(element);
		visualById.Add(id, visual);
		foreach (var child in GetChildren(visual))
		{
			AddNode(element, child, ref id, visualById);
		}
	}
	private static XElement CreateNode(Visual visual, int id)
	{
		var element = new XElement(visual.GetType().Name);
		element.SetAttributeValue("Id", id);
		if (!string.IsNullOrEmpty(visual.Name))
		{
			element.SetAttributeValue("Name", visual.Name);
		}
		if (visual.GetValue(AutomationProperties.AutomationIdProperty) is { } automationId && !string.IsNullOrEmpty(automationId))
		{
			element.SetAttributeValue("AutomationId", automationId);
		}
		if (GetText(visual) is { } text && !string.IsNullOrEmpty(text))
		{
			element.SetAttributeValue("Text", text);
		}
		return element;
	}
	private static string GetText(Visual visual)
	{
		return visual switch
		{
			ToolbarItem tbi => tbi.Header?.ToString(),
			TextBlock tbl => tbl.Text,
			TextBox tb => tb.Text,
			TextEditor te => te.DisplayText,
			ContentControl cc => cc.Content?.ToString(),
			PropertyGridRowControl pgr => pgr.Row?.FullPropertyPath,
			_ => ""
		};
	}
	private static IEnumerable<Visual> GetChildren(Visual visual)
	{
		return visual switch
		{
			Popup popup => [ popup.Child ],
			_ => visual.GetVisualChildren()
		};
	}
}