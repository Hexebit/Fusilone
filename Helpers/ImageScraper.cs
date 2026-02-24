using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Fusilone.Helpers;

public class ImageScraper
{
    private static readonly HttpClient _httpClient = new HttpClient();

    public static async Task<string?> GetDeviceImageUrl(string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return null;

        string normalized = NormalizeQuery(query);
        if (!IsSearchable(normalized)) return null;

        var queries = new[]
        {
            normalized,
            normalized + " product photo",
            normalized + " device"
        };

        foreach (var q in queries.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            // Wikipedia (Often has high quality, reliable product images)
            string? url = await GetWikipediaImage(q);
            if (!string.IsNullOrEmpty(url)) return url;

            // Bing Images
            url = await GetBingImage(q);
            if (!string.IsNullOrEmpty(url)) return url;
        }

        return null;
    }

    private static string NormalizeQuery(string query)
    {
        // Remove extra spaces and common separators
        string cleaned = Regex.Replace(query, @"\s+", " ").Trim();
        cleaned = Regex.Replace(cleaned, @"\bFSL-[A-Z]{2}-\d+\b", "", RegexOptions.IgnoreCase).Trim();
        return cleaned;
    }

    private static bool IsSearchable(string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return false;

        string digitsOnly = Regex.Replace(query, @"[^\d]", "");
        if (digitsOnly.Length >= 15 || (query.Length > 5 && query.All(char.IsDigit)))
        {
            return false;
        }

        return query.Length >= 3;
    }

    private static async Task<string?> GetWikipediaImage(string query)
    {
        try
        {
            // Search Wikipedia API for pages
            string searchApi = $"https://en.wikipedia.org/w/api.php?action=query&list=search&srsearch={Uri.EscapeDataString(query)}&format=json";
            string searchJson = await _httpClient.GetStringAsync(searchApi);
            
            // Extract first page ID (Simple Regex for speed, avoid heavy JSON dep if possible, but we use System.Text.Json elsewhere so regex is fine for simple extraction)
            var match = Regex.Match(searchJson, "\"pageid\":(\\d+)");
            if (match.Success)
            {
                string pageId = match.Groups[1].Value;
                // Get page images
                string imgApi = $"https://en.wikipedia.org/w/api.php?action=query&prop=pageimages&pageids={pageId}&pithumbsize=500&format=json";
                string imgJson = await _httpClient.GetStringAsync(imgApi);
                
                var urlMatch = Regex.Match(imgJson, "\"source\":\"(https://[^\"]+)\"");
                if (urlMatch.Success)
                {
                    return urlMatch.Groups[1].Value;
                }
            }
            return null;
        }
        catch { return null; }
    }

    private static async Task<string?> GetBingImage(string query)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
            string url = $"https://www.bing.com/images/search?q={Uri.EscapeDataString(query)}&first=1";
            string html = await _httpClient.GetStringAsync(url);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Prefer murl from iusc nodes (full-res)
            var iusc = doc.DocumentNode.Descendants("a")
                .FirstOrDefault(a => a.GetAttributeValue("class", "").Contains("iusc") &&
                                     a.GetAttributeValue("m", "").Contains("murl"));

            if (iusc != null)
            {
                var m = iusc.GetAttributeValue("m", "");
                var match = Regex.Match(m, "\"murl\":\"(https?:\\/\\/[^\"]+)\"");
                if (match.Success)
                {
                    return match.Groups[1].Value.Replace("\\/", "/");
                }
            }

            // Fallback to thumbnail
            var imgNode = doc.DocumentNode.Descendants("img")
                .FirstOrDefault(img =>
                    img.GetAttributeValue("class", "").Contains("mimg") &&
                    img.GetAttributeValue("src", "").StartsWith("http"));

            return imgNode?.GetAttributeValue("src", string.Empty);
        }
        catch { return null; }
    }
}
