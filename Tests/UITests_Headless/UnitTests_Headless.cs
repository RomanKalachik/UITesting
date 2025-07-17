using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Headless;
using Avalonia.Headless.XUnit;
using MortgageCalculator.ViewModels;
using MortgageCalculator.Views;

namespace UITests;

public class UnitTests_Headless
{

    private static MainWindow CreateWindow()
    {
        return new MainWindow
        {
            DataContext = new MortgageCalculatorViewModel()
        };
    }

    [AvaloniaFact]
    public void Simple_Show()
    {
        MainWindow window = CreateWindow();
        window.Show();
        var frame = window.CaptureRenderedFrame();
        frame.Save("simple.png");
    }

    [AvaloniaFact]
    public void Simple_Interactions()
    {
        MainWindow window = CreateWindow();
        window.Show();
        MortgageCalculatorView? view = window.Content as MortgageCalculatorView;
        Assert.NotNull(view);
        view.isVisitorCheckBox.IsChecked = true;
        Assert.Equal("13%", view.annualInterestRateLabel.Content);
    }

    [AvaloniaFact]
    public void Simple_Window_Bounds()
    {
        MainWindow window = CreateWindow();
        window.Show();
        Assert.Equal(0, window.Position.X);
        Assert.Equal(0, window.Position.Y);
    }

}
