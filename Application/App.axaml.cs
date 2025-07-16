using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using MortgageCalculator.ViewModels;
using MortgageCalculator.Views;
using System.Linq;
using System.Runtime.InteropServices;

namespace MortgageCalculator;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MortgageCalculatorViewModel() { IsLinuxUser = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) }
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}