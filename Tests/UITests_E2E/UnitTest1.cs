using Avalonia.Controls;
using MortgageCalculator.Views;
using UITests_E2E.Fixture;

namespace UITests;

public class UnitTest1
{
    [Fact]
    public async Task SimpleTest()
    {
        Window window = new MainWindow();
        await window.ShowDialog(AvaloniaApp.GetMainWindow());
    }
}
