using System.Runtime.CompilerServices;

namespace UITests_E2E.E2E_Adapters;

public static class ScreenshotHelper
{
	public static async Task SaveScreenshotAsync(this InputElementAdapter adapter, [CallerMemberName()] string memberName = null)
	{
		var storage = AmbientContext.Get<IScreenshotStorage>(nameof(IScreenshotStorage));
		if (storage != null)
		{
			using var screenshot = await adapter.TakeScreenshotAsync();
			storage.SaveScreenshot(screenshot, memberName);
		}
	}
}