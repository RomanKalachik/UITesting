using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using MortgageCalculator.ViewModels;
using MortgageCalculator.Views;

namespace UITests_E2E;

public class UnitTests_E2E_Simple
{

    private static MainWindow CreateAndShowWindow(bool show = true)
    {
        var result = new MainWindow
        {
            DataContext = new MortgageCalculatorViewModel()
        };
        if(show) result.Show();
        return result;
    }

    //[Fact]
    //public async Task Simple_Show()
    //{
    //    MainWindow window = CreateAndShowWindow(false);
    //    await window.ShowDialog((Application.Current.ApplicationLifetime as ClassicDesktopStyleApplicationLifetime).MainWindow);

    //    window.Close();
    //}

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
