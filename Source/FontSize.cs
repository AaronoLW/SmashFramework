using System.Drawing;
using System.Numerics;
using SDL3;
using SmashFramework;

internal class FontSize : IDisposable
{
    public readonly nint Handle;

    public FontSize(string path, float pointSize)
    {
        Handle = TTF.OpenFont(path, pointSize);
    }

    ~FontSize()
    {
        Dispose();
    }

    public Vector2 MeasureString(string text, TextEngine textEngine)
    {
        nint textObject = CreateText(text, textEngine);
        TTF.GetTextSize(textObject, out int w, out int h);

        TTF.DestroyText(textObject);
        return new(w, h);
    }

    internal void RenderString(TextEngine textEngine, string text, Vector2 position, Color color)
    {
        nint textObject = CreateText(text, textEngine);
        TTF.SetTextColor(textObject, color.R, color.G, color.B, color.A);

        TTF.DrawRendererText(textObject, position.X, position.Y);
        TTF.DestroyText(textObject);
    }

    private nint CreateText(string text, TextEngine textEngine)
    {
        return TTF.CreateText(textEngine._handle, Handle, text, 0);
    }

    public void Dispose()
    {
        TTF.CloseFont(Handle);
    }
}
