using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SmashFramework;

/// <summary>
/// Helper class to get absolute font paths from a font name
/// </summary>
public static class SystemFontResolver
{
    /// <summary>
    /// Resolves a system font name to an absolute file path.
    /// </summary>
    public static string? ResolveByName(string fontName)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            return GetLinuxFontPath(fontName);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return GetWindowsFontPath(fontName);

        throw new PlatformNotSupportedException("Operating system not supported.");
    }

    public static string[]? ResolveByFamily(string fontFamily)
    {
        if (!(RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD)))
            throw new PlatformNotSupportedException("Operating system not supported.");

        try
        {
            ProcessStartInfo info = new()
            {
                FileName = "/bin/bash",
                Arguments = "-c \"fc-list -f '%{file}\n'" + $" | grep -i '{fontFamily}'\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(info);
            if (process != null)
            {
                string path = process.StandardOutput.ReadToEnd().Trim();
                process.WaitForExit();

                return path.Split('\n');
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Warning] Failed to run fc-match: {ex.Message}");
        }

        return null;
    }

    private static string? GetLinuxFontPath(string fontName)
    {
        try
        {
            ProcessStartInfo info = new()
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"fc-list : file | grep -i {fontName}.ttf\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(info);
            if (process != null)
            {
                string path = process.StandardOutput.ReadToEnd().Trim();
                process.WaitForExit();

                if (path.EndsWith(":"))
                    path = path[0..(path.Length - 1)];

                if (File.Exists(path))
                    return path;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Warning] Failed to run fc-match: {ex.Message}");
        }

        return null;
    }

    private static string? GetWindowsFontPath(string fontName)
    {
        if (!OperatingSystem.IsWindows())
            return null;

        string jrbFolder = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\Microsoft\Windows\Fonts");

        string fontPath = Path.Combine(jrbFolder, fontName + ".ttf");

        if (File.Exists(fontPath))
            return fontPath;
        else
            return null;
    }
}
