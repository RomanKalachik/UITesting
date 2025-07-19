using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Eremex.AvaloniaUI.Controls.Editors;
using UITests_E2E.E2E_Adapters;

namespace UITests_E2E;

public class UnitTests_E2E
{
    public MainWindowAdapter RootAdapter { get; private set; }

    public async Task Init()
    {
        RootAdapter = new MainWindowAdapter(((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).MainWindow);
        await RootAdapter.WaitUntilLoadedAsync();

    }

    [Fact]
    public async Task Test_PromoCode_KeyTextInput() 
    {
        await Init();
        var textEditorLocator = By.InputElementLocator<TextEditor>(RootAdapter, e=> e.Name == "promocodeText");
        var textEditorAdapter = await textEditorLocator.WaitUntilVisibleAsync<TextEditorAdapter>();
        
        var labelLocator = By.InputElementLocator<Label>(RootAdapter, e=> e.Name == "annualInterestRateLabel");
        var labelAdapter = await labelLocator.WaitUntilVisibleAsync<LabelAdapter>();

        await textEditorAdapter.SendTextAsync("sale", true);
        await labelAdapter.WaitUntilTextPresentAsync("12%");
        await textEditorAdapter.SelectAllAsync();
        await textEditorAdapter.SendTextAsync(" ", true);
    }

    [Fact]
    public async Task Test_Visitor()
    {
        await Init();

        var checkBoxLocator = By.InputElementLocator<CheckEditor>(RootAdapter, x => x.Name == "isVisitorCheckBox");
        var checkBoxAdapter = await checkBoxLocator.WaitUntilLoadedAsync<ToggleButtonAdapter>();

        var labelLocator = By.InputElementLocator<Label>(RootAdapter, e => e.Name == "annualInterestRateLabel");
        var labelAdapter = await labelLocator.WaitUntilVisibleAsync<LabelAdapter>();

        await checkBoxAdapter.Check();
        await labelAdapter.WaitUntilTextPresentAsync("13%");
        await checkBoxAdapter.Uncheck();
    }
}
