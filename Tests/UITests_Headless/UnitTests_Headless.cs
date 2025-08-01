using Avalonia.Headless;
using Avalonia.Headless.XUnit;
using MortgageCalculator.ViewModels;
using MortgageCalculator.Views;

namespace UITests;

public class UnitTests_Headless
{

    private static MainWindow CreateAndShowWindow()
    {
        var result = new MainWindow
        {
            DataContext = new MortgageCalculatorViewModel()
        };
        result.Show();
        return result;
    }

    [AvaloniaFact]
    public void Simple_Show()
    {
        MainWindow window = CreateAndShowWindow();
        window.Show();
        var frame = window.CaptureRenderedFrame();
        frame.Save("!simple.png");
    }

   [AvaloniaFact]
    public void Simple_Window_Bounds()
    {
        MainWindow window = CreateAndShowWindow();
        window.Show();
        Assert.Equal(0, window.Position.X);
        Assert.Equal(0, window.Position.Y);
    }


    [AvaloniaFact]
    public void Test_OS_Linux()
    {
        MainWindow window = CreateAndShowWindow();
        Assert.True((window.Content as MortgageCalculatorView)?.isLinuxUser.IsChecked);
    }

    [AvaloniaFact]
    public void Test_OS_Windows()
    {
        MainWindow window = CreateAndShowWindow();
        Assert.False((window.Content as MortgageCalculatorView)?.isLinuxUser.IsChecked);
    }

    [AvaloniaFact]
    public void Test_Visitor()
    {
        MainWindow window = CreateAndShowWindow();
        MortgageCalculatorView view = window.Content as MortgageCalculatorView;
        view.isVisitorCheckBox.IsChecked = true;
        Assert.Equal("13%", view.annualInterestRateLabel.Content);
    }

    [AvaloniaFact]
    public void Test_PromoCode()
    {
        MainWindow window = CreateAndShowWindow();
        MortgageCalculatorView view = window.Content as MortgageCalculatorView;
        view.promocodeText.EditorValue = "sale";
        Assert.Equal("12%", view.annualInterestRateLabel.Content);
    }

    [AvaloniaFact]
    public void Test_PromoCode_KeyTextInput()
    {
        MainWindow window = CreateAndShowWindow();
        MortgageCalculatorView view = window.Content as MortgageCalculatorView;
        window.Activate();
        view.promocodeText.Focus();
        window.KeyTextInput("sale");
        Assert.Equal("12%", view.annualInterestRateLabel.Content);
    }
}
