using Avalonia.Media.Imaging;

namespace UITests_E2E.E2E_Adapters;
public interface IScreenshotStorage
{
	void SaveScreenshot(Bitmap screenshot, string screenshotName);
}