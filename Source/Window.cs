using System.Numerics;
using SDL3;

namespace SmashFramework;

public class Window : IDisposable
{
    public readonly nint Handle;

    public float Width { get { SDL.GetWindowSize(Handle, out int width, out _); return width; } }
    public float Height { get { SDL.GetWindowSize(Handle, out _, out int height); return height; } }
    public Vector2 Size { get { SDL.GetWindowSize(Handle, out int width, out int height); return new(width, height); } }

    public Window(nint windowHandle)
    {
        Handle = windowHandle;
    }

    public Window(string title, int width, int height, params WindowFlag[] windowFlags)
    {
        SDL.WindowFlags flags = windowFlags.Length > 0 ? windowFlags.Cast<SDL.WindowFlags>().Aggregate((a, b) => a | b) : 0;
        Handle = SDL.CreateWindow(title, width, height, flags);
    }

    private bool _pollingTextInput = false;

    public void StartPollingTextInput()
    {
        SDL.StartTextInput(Handle);
        _pollingTextInput = true;
    }

    public void Dispose()
    {
        if (_pollingTextInput)
            SDL.StopTextInput(Handle);

        SDL.DestroyWindow(Handle);
    }
}
