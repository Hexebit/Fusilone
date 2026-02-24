using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Fusilone.Data;
using Fusilone.Models;

namespace Fusilone;

public partial class MaintenanceWindow : Window
{
    private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

    public MaintenanceWindow()
    {
        InitializeComponent();
        LoadMaintenanceData();
    }

    private void LoadMaintenanceData()
    {
        try
        {
            var devices = _dbHelper.GetAllDevices();
            
            // Sort by NextMaintenanceDate Ascending (Earliest/Overdue first)
            var sortedDevices = devices.OrderBy(d => d.NextMaintenanceDate).ToList();
            
            MaintenanceDataGrid.ItemsSource = sortedDevices;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Veriler yüklenirken hata oluştu: {ex.Message}");
        }
    }

    private void MaintenanceDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (MaintenanceDataGrid.SelectedItem is Device selectedDevice)
        {
            var detailWindow = new DeviceDetailWindow(selectedDevice);
            detailWindow.Owner = this;
            detailWindow.ShowDialog();
            
            // Dönüşte listeyi yenile (Bakım eklenmiş olabilir)
            LoadMaintenanceData();
        }
    }
}
