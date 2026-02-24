using System;
using System.IO;

namespace Fusilone.Helpers;

public static class PathHelper
{
    private static readonly string AppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Fusilone");

    public static string GetAppDataPath()
    {
        EnsureDirectory(AppDataPath);
        return AppDataPath;
    }

    public static string GetDatabasePath()
    {
        string dataPath = Path.Combine(AppDataPath, "Data");
        EnsureDirectory(dataPath);
        return Path.Combine(dataPath, "fusilone.db");
    }

    public static string GetImagesPath()
    {
        string imagesPath = Path.Combine(AppDataPath, "Images");
        EnsureDirectory(imagesPath);
        return imagesPath;
    }

    public static string GetLabelsPath()
    {
        string labelsPath = Path.Combine(AppDataPath, "Etiketler");
        EnsureDirectory(labelsPath);
        return labelsPath;
    }

    public static string GetAssetsPath()
    {
        // Assets are read-only resources packaged with the app, so we keep using BaseDirectory for reading the logo
        // However, if we wanted to allow user custom logo, we'd copy it to MyDocuments.
        // For now, let's keep the logo in the app directory for reliability.
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets");
    }

    private static void EnsureDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}
