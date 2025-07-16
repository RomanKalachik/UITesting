using Avalonia.Controls;
using MortgageCalculator.Views;

namespace UITests;

public class UnitTest1
{
    [Fact]
    public async Task SimpleTest()
    {
        Window window = new MainWindow();
        await window.ShowDialog(null);
    }
}
