using System.Drawing;
using System.Numerics;
using SDL3;

internal class FontSize : IDisposable
{
    public nint Handle { get; private set; }

    public FontSize(string path, float pointSize)
    {
        Handle = TTF.OpenFont(path, pointSize);
    }

    public Vector2 MeasureString(string text, TextEngine textEngine)
    {
        nint textObject = CreateText(text, textEngine);

        if (textObject == 0)
            throw new InvalidOperationException($"MeasureString: Failed to create text object: {SDL.GetError()}");

        TTF.GetTextSize(textObject, out int w, out int h);

        TTF.DestroyText(textObject);
        return new(w, h);
    }

    internal void RenderString(TextEngine textEngine, string text, Vector2 position, Color color)
    {
        nint textObject = CreateText(text, textEngine);

        if (textObject == 0)
            throw new InvalidOperationException($"RenderString: Failed to create text object: {SDL.GetError()}");

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
        Handle = 0;
    }
}
