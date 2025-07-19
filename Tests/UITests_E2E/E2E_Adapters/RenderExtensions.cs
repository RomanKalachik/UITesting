using Avalonia;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using SkiaSharp;

namespace UITests_E2E.E2E_Adapters;

public static class RenderExtensions
{
	public static Bitmap RenderToBitmap(this InputElement target)
	{
		ArgumentNullException.ThrowIfNull(target);
		var size = new Size(target.Bounds.Width, target.Bounds.Height);
		var pixelSize = new PixelSize((int)size.Width, (int)size.Height);
		var bitmap = new RenderTargetBitmap(pixelSize);
		target.Measure(size);
		target.Arrange(new Rect(size));
		bitmap.Render(target);
		return bitmap;
	}

	public static void SaveDiffImage(string expectedImage, string actualImage, string diffImage)
	{
		using var expected = SKBitmap.Decode(expectedImage);
		using var actual = SKBitmap.Decode(actualImage);
		using var diff = GetDiffImage(expected, actual);
		using var data = diff.Encode(SKEncodedImageFormat.Png, 100);
		using var file = File.Create(diffImage);
		data.SaveTo(file);
	}
	public static SKBitmap GetDiffImage(SKBitmap expected, SKBitmap actual)
	{
		var width = Math.Max(expected.Width, actual.Width);
		var height = Math.Max(expected.Height, actual.Height);
		var diffColor = new SKColor(255, 0, 0);
		var diff = new SKBitmap(width, height);

		for (var x = 0; x < width; x++)
		{
			for (var y = 0; y < height; y++)
			{
				var color = diffColor;
				if (x < expected.Width && y < expected.Height && x < actual.Width && y < actual.Height && expected.GetPixel(x, y) == actual.GetPixel(x, y))
				{
					color = ToTransparent(expected.GetPixel(x, y));
					
				}
				diff.SetPixel(x, y, color);
			}
		}

		return diff;

		static SKColor ToTransparent(SKColor color) => new SKColor(ToTransparentColor(color.Red), ToTransparentColor(color.Green), ToTransparentColor(color.Blue), color.Alpha);
		static byte ToTransparentColor(byte color)
		{
			const double opacity = 0.50;
			const byte background = 255;
			return (byte)(opacity * color + (1 - opacity) * background);
		}
	}
}
