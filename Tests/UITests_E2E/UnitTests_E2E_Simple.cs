using Avalonia.Controls;
using Avalonia.Input;
using Eremex.AvaloniaUI.Controls.Editors;
using MortgageCalculator.ViewModels;
using MortgageCalculator.Views;

namespace UITests_E2E;

public class UnitTests_E2E_Simple
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

    [Fact]
    public void Simple_Show()
    {
        MainWindow window = CreateAndShowWindow();
        window.Show();
        //TODO

        window.Close();
    }

    [Fact]
    public void Simple_Window_Bounds()
    {
        MainWindow window = CreateAndShowWindow();
        window.Show();
        Assert.Equal(0, window.Position.X);
        Assert.Equal(0, window.Position.Y);
        window.Close();
    }


    [Fact]
    public void Test_OS_Linux()
    {
        MainWindow window = CreateAndShowWindow();
        Assert.True((window.Content as MortgageCalculatorView)?.isLinuxUser.IsChecked);
        window.Close();

    }

    [Fact]
    public void Test_OS_Windows()
    {
        MainWindow window = CreateAndShowWindow();
        Assert.False((window.Content as MortgageCalculatorView)?.isLinuxUser.IsChecked);
        window.Close();

    }

    [Fact]
    public void Test_Visitor()
    {
        MainWindow window = CreateAndShowWindow();
        MortgageCalculatorView view = window.Content as MortgageCalculatorView;
        view.isVisitorCheckBox.IsChecked = true;
        Assert.Equal("13%", view.annualInterestRateLabel.Content);
        window.Close();

    }

    [Fact]
    public void Test_PromoCode()
    {
        MainWindow window = CreateAndShowWindow();
        MortgageCalculatorView view = window.Content as MortgageCalculatorView;
        view.promocodeText.EditorValue = "sale";
        Assert.Equal("12%", view.annualInterestRateLabel.Content);
        window.Close();

    }
}
