using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Fusilone.Models;

namespace Fusilone.Views;

public partial class DeviceKanbanView : UserControl
{
    public DeviceKanbanView()
    {
        InitializeComponent();
    }

    public void SetDevices(List<Device> devices)
    {
        // 1. Bekleyen (Pasif)
        ListPending.ItemsSource = devices.Where(d => d.Status == "Pasif").ToList();

        // 2. İşlemde (Arızalı)
        ListInProgress.ItemsSource = devices.Where(d => d.Status == "Arızalı").ToList();

        // 3. Hazır (Aktif)
        ListReady.ItemsSource = devices.Where(d => d.Status == "Aktif").ToList();
    }

    private void Card_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (sender is FrameworkElement element && element.DataContext is Device device)
        {
            var detailWindow = new DeviceDetailWindow(device);
            detailWindow.Owner = Application.Current.MainWindow;
            detailWindow.ShowDialog();
            
            // Refresh parent view if possible (Not strictly MVVM but works for this scope)
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                // Trigger refresh logic on main window
                // Since this method isn't public, we rely on the user refreshing via UI or 
                // implementing an event. For now, just opening detail is good.
            }
        }
    }
}
