using System.Numerics;
using SDL3;

namespace SmashFramework;

public class Window(nint windowHandle) : IDisposable
{
    public readonly nint Handle = windowHandle;

    public float Width { get { SDL.GetWindowSize(Handle, out int width, out _); return width; } }
    public float Height { get { SDL.GetWindowSize(Handle, out _, out int height); return height; } }
    public Vector2 Size { get { SDL.GetWindowSize(Handle, out int width, out int height); return new(width, height); } }

    private bool _pollingTextInput = false;

    public static Window Create(string title, int width, int height, params SDL.WindowFlags[] windowFlags)
    {
        return new Window(SDL.CreateWindow(title, width, height, windowFlags.Aggregate((a, b) => a | b)));
    }

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
