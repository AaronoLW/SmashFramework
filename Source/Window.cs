using System.Numerics;
using SDL3;

namespace SmashFramework;

public class Window(nint windowHandle) : IDisposable
{
    public readonly nint Handle = windowHandle;

    public float Width { get { SDL.GetWindowSize(Handle, out int width, out _); return width; } }
    public float Height { get { SDL.GetWindowSize(Handle, out _, out int height); return height; } }
    public Vector2 Size { get { SDL.GetWindowSize(Handle, out int width, out int height); return new(width, height); } }

    public static Window Create(string title, int width, int height, params SDL.WindowFlags[] windowFlags)
    {
        return new Window(SDL.CreateWindow(title, width, height, windowFlags.Aggregate((a, b) => a | b)));
    }

    public void SetFullscreen(bool fullscreen)
    {
        SDL.SetWindowFullscreen(Handle, fullscreen);
    }

    public void SetWindowAlwaysOnTop(bool alwaysOnTop)
    {
        SDL.SetWindowAlwaysOnTop(Handle, alwaysOnTop);
    }

    public void SetWindowTitle(string title)
    {
        SDL.SetWindowTitle(Handle, title);
    }

    public void SetWindowMinimumSize(int minimumWidth, int minimumHeight)
    {
        SDL.SetWindowMinimumSize(Handle, minimumWidth, minimumHeight);
    }

    public void SetWindowMaximumSize(int maximumWidth, int maximumHeight)
    {
        SDL.SetWindowMaximumSize(Handle, maximumWidth, maximumHeight);
    }

    public void SetWindowResizable(bool resizable)
    {
        SDL.SetWindowResizable(Handle, resizable);
    }

    public void Dispose()
    {
        SDL.DestroyWindow(Handle);
    }
}
