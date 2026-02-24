using System.Diagnostics;
using System.Windows;

namespace Fusilone;

public partial class AboutWindow : Window
{
    public AboutWindow()
    {
        InitializeComponent();
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void OpenGitHub_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/Hexebit",
                UseShellExecute = true
            });
        }
        catch { /* Silently handle if browser fails to open */ }
    }
}
