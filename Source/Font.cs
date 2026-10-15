using System.Drawing;
using System.Numerics;
using SDL3;
using SmashFramework;

public class Font : IDisposable
{
    public readonly string Path;
    public float PointSize
    {
        get => TTF.GetFontSize(_fontHandle);
        set { SetFontSize(value); }
    }

    private readonly TextEngine _textEngine;

    internal nint _fontHandle { get; private set; }

    public Font(string path, float initialPointSize, Renderer renderer)
    {
        Path = path;
        _fontHandle = TTF.OpenFont(path, initialPointSize);
        _textEngine = new(renderer);
    }

    public Vector2 MeasureString(string text)
    {
        nint textObject = CreateText(text);
        TTF.GetTextSize(textObject, out int w, out int h);

        TTF.DestroyText(textObject);
        return new(w, h);
    }

    internal void RenderString(string text, Vector2 position, Color color)
    {
        nint textObject = CreateText(text);
        TTF.SetTextColor(textObject, color.R, color.G, color.B, color.A);

        TTF.DrawRendererText(textObject, position.X, position.Y);
        TTF.DestroyText(textObject);
    }

    private nint CreateText(string text)
    {
        return TTF.CreateText(_textEngine._handle, _fontHandle, text, 0);
    }

    private void SetFontSize(float pointSize)
    {
        if (pointSize != PointSize)
        {
            TTF.CloseFont(_fontHandle);
            _fontHandle = TTF.OpenFont(Path, pointSize);
        }
    }

    public void Dispose()
    {
        _textEngine.Dispose();
        TTF.CloseFont(_fontHandle);
    }
}
