using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Fusilone.Helpers;

public static class SpecLocalization
{
    private static readonly Dictionary<string, string> TrToEn = new(StringComparer.OrdinalIgnoreCase)
    {
        { "CPU Markası", "CPU Brand" },
        { "CPU Modeli", "CPU Model" },
        { "Anakart Markası", "Motherboard Brand" },
        { "Anakart Modeli", "Motherboard Model" },
        { "RAM Türü", "RAM Type" },
        { "İşletim Sistemi", "Operating System" },
        { "RAM Boyutu (Toplam)", "Total RAM Size" },
        { "Hafıza (Toplam)", "Total Storage" },
        { "Dahili Ekran Kartı Markası", "Integrated GPU Brand" },
        { "Dahili Ekran Kartı Modeli", "Integrated GPU Model" },
        { "Dahili Ekran Kartı Bellek Miktarı", "Integrated GPU Memory" },
        { "Harici Ekran Kartı Markası", "Dedicated GPU Brand" },
        { "Harici Ekran Kartı Modeli", "Dedicated GPU Model" },
        { "Harici Ekran Kartı Bellek Miktarı", "Dedicated GPU Memory" },
        { "RAM Markası", "RAM Brand" },
        { "RAM Modeli (Slot 1)", "RAM Model (Slot 1)" },
        { "RAM Modeli (Slot 2)", "RAM Model (Slot 2)" },
        { "RAM Modeli (Slot 3)", "RAM Model (Slot 3)" },
        { "RAM Modeli (Slot 4)", "RAM Model (Slot 4)" },
        { "Ana Depolama Cihazı Markası", "Primary Storage Brand" },
        { "Ana Depolama Cihazı Modeli", "Primary Storage Model" },
        { "Ana Depolama Cihazı Türü", "Primary Storage Type" },
        { "İkincil Depolama Cihazı Markası", "Secondary Storage Brand" },
        { "İkincil Depolama Cihazı Modeli", "Secondary Storage Model" },
        { "İkincil Depolama Cihazı Türü", "Secondary Storage Type" },
        { "PSU Markası", "PSU Brand" },
        { "PSU Modeli", "PSU Model" },
        { "PSU Gücü", "PSU Wattage" },
        { "Soğutma Tipi", "Cooling Type" },
        { "DVD-CD Sürücü Markası", "DVD/CD Drive Brand" },
        { "DVD-CD Sürücü Modeli", "DVD/CD Drive Model" },
        { "BIOS versiyonu", "BIOS Version" },
        { "Wifi Desteği", "Wi-Fi Support" },
        { "Bluetooth Desteği", "Bluetooth Support" },
        { "Güç Adaptörü Markası", "Power Adapter Brand" },
        { "Güç Adaptörü Modeli", "Power Adapter Model" },
        { "Adaptör Gücü", "Adapter Wattage" },
        { "Monitör Panel Tipi", "Display Panel Type" },
        { "Monitör Ekran Çözünürlüğü", "Display Resolution" },
        { "Kamera desteği", "Camera Support" },
        { "Ekran Çözünürlüğü", "Screen Resolution" },
        { "Ekran Boyutu", "Screen Size" },
        { "Panel Tipi", "Panel Type" },
        { "Desteklenen Görüntü Portları", "Supported Display Ports" },
        { "Adaptör Türü", "Adapter Type" },
        { "Hoparlör özelliğinin olup olmadığı", "Speaker Support" },
        { "Yenileme Hızı", "Refresh Rate" },
        { "Ram Miktarı", "RAM Size" },
        { "Depolama Miktarı", "Storage Size" },
        { "Batarya Kapasitesi", "Battery Capacity" },
        { "IMEI", "IMEI" },
        { "Şarj Port Türü", "Charging Port Type" },
        { "Yazıcı Türü (Toner ya da Kartuş)", "Printer Type (Toner/Cartridge)" },
        { "Renk Desteği", "Color Support" },
        { "Fonksiyonlar (Sadece yazıcı ya da Yazıcı ve Tarayıcı gibi)", "Functions (Printer/Printer+Scanner)" },
        { "Sarf Malzemesi Modeli", "Consumable Model" },
        { "Bağlantı Türü", "Connection Type" },
        { "Router Türü (Modem, Router ya da Bridge gibi)", "Router Type (Modem/Router/Bridge)" },
        { "WanType", "WAN Type" },
        { "Admin kullanıcı adı", "Admin Username" },
        { "wifi Bandı", "Wi-Fi Band" },
        { "Çözünürlük", "Resolution" },
        { "GPU", "GPU" },
        { "RAM", "RAM" },
        { "CPU", "CPU" },
        { "Storage", "Storage" }
    };

    private static readonly Dictionary<string, string> StatusTrToEn = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Aktif", "Active" },
        { "Pasif", "Inactive" },
        { "Arızalı", "Faulty" },
        { "Hurda", "Scrap" },
        { "Kayıp", "Lost" }
    };

    public static string GetDisplayLabel(string key)
    {
        if (!IsEnglish()) return key;
        return TrToEn.TryGetValue(key, out var en) ? en : key;
    }

    public static string GetStatusDisplayLabel(string status)
    {
        if (!IsEnglish()) return status;
        return StatusTrToEn.TryGetValue(status, out var en) ? en : status;
    }

    private static bool IsEnglish()
    {
        var app = Application.Current;
        if (app?.Resources?.MergedDictionaries == null) return false;

        return app.Resources.MergedDictionaries.Any(d => d.Source != null &&
            d.Source.OriginalString.IndexOf("/Languages/English.xaml", StringComparison.OrdinalIgnoreCase) >= 0);
    }
}
