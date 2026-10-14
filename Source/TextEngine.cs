using System.Drawing;
using System.Numerics;
using SDL3;
using SmashFramework;

internal class TextEngine(Renderer renderer) : IDisposable
{
    private readonly nint _handle = TTF.CreateRendererTextEngine(renderer.Handle);

    public nint CreateText(Font font, string text)
    {
        return TTF.CreateText(_handle, font._fontHandle, text, 0);
    }

    public void RenderString(Font font, string text, Vector2 position, Color color)
    {
        nint textObject = CreateText(font, text);
        TTF.SetTextColor(textObject, color.R, color.G, color.B, color.A);

        TTF.DrawRendererText(textObject, position.X, position.Y);
        TTF.DestroyText(textObject);
    }

    public void Dispose()
    {
        TTF.DestroyRendererTextEngine(_handle);
    }
}
