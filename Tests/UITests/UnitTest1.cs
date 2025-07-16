using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Headless.XUnit;

namespace UITests;

public class UnitTest1
{
    [AvaloniaFact]
    public void Should_Type_Text_Into_TextBox()
    {
        // Setup controls:
        var textBox = new TextBox();
        var window = new Window { Content = textBox };

        // Open window:
        window.Show();

        // Focus text box:
        textBox.Focus();

        // Simulate text input:
        window.KeyTextInput("Hello World");

        // Assert:
    }
}
