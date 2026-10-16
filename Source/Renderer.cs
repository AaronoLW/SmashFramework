using System.Drawing;
using System.Numerics;
using SDL3;

namespace SmashFramework;

public class Renderer : IDisposable
{
    /// <summary>
    /// The handle of the SDL renderer
    /// </summary>
    public readonly nint Handle;

    internal TextEngine _textEngine { get; private set; }

    public Renderer(Window window)
    {
        Handle = SDL.CreateRenderer(window.Handle, null);
        _textEngine = new(this);
    }

    /// <summary>
    /// Refreshes the internal TextEngine used for text rendering.
    /// This can lower RAM usage if you are drawing a lot of Text.
    /// This should never be called before RenderPresent().
    /// </summary>
    public void RefreshTextEngine()
    {
        _textEngine.Dispose();
        _textEngine = new(this);
    }

    /// <summary>
    /// Clears the screen with the specified color
    /// </summary>
    public void Clear(Color color)
    {
        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, 1);
        SDL.RenderClear(Handle);
    }

    /// <summary>
    ///Renders a Texture to the screen
    /// </summary>
    public void RenderTexture(Texture2D texture, Vector2 position, Color color, float scale = 1)
    {
        SDL.FRect dstRect = new SDL.FRect
        {
            X = position.X,
            Y = position.Y,
            W = texture.Width * scale,
            H = texture.Height * scale
        };

        if (texture._modulatedColor.R != color.R ||
            texture._modulatedColor.G != color.G ||
            texture._modulatedColor.B != color.B ||
            texture._modulatedColor.A != color.A)
        {
            SDL.SetTextureColorMod(texture.Handle, color.R, color.G, color.B);
            SDL.SetTextureAlphaMod(texture.Handle, color.A);
            texture._modulatedColor = color;
        }

        SDL.RenderTexture(Handle, texture.Handle, texture._sourceRectangle, dstRect);
    }

    /// <summary>
    ///Renders a Texture to the screen
    /// </summary>
    public void RenderTexture(Texture2D texture, float x, float y, Color color, float scale = 1)
    {
        SDL.FRect dstRect = new SDL.FRect
        {
            X = x,
            Y = y,
            W = texture.Width * scale,
            H = texture.Height * scale
        };

        if (texture._modulatedColor.R != color.R ||
            texture._modulatedColor.G != color.G ||
            texture._modulatedColor.B != color.B ||
            texture._modulatedColor.A != color.A)
        {
            SDL.SetTextureColorMod(texture.Handle, color.R, color.G, color.B);
            SDL.SetTextureAlphaMod(texture.Handle, color.A);
            texture._modulatedColor = color;
        }

        SDL.RenderTexture(Handle, texture.Handle, texture._sourceRectangle, dstRect);
    }

    /// <summary>
    ///Renders a Texture to the screen but with rotation
    /// </summary>
    public void RenderTextureRotated(Texture2D texture, Vector2 position, Color color, double angle, float scale = 1)
    {
        SDL.FRect rect = new SDL.FRect
        {
            X = position.X,
            Y = position.Y,
            W = texture.Width * scale,
            H = texture.Height * scale
        };

        if (texture._modulatedColor.R != color.R ||
            texture._modulatedColor.G != color.G ||
            texture._modulatedColor.B != color.B ||
            texture._modulatedColor.A != color.A)
        {
            SDL.SetTextureColorMod(texture.Handle, color.R, color.G, color.B);
            SDL.SetTextureAlphaMod(texture.Handle, color.A);
            texture._modulatedColor = color;
        }

        SDL.RenderTextureRotated(Handle, texture.Handle, texture._sourceRectangle, rect, angle, IntPtr.Zero, SDL.FlipMode.None);
    }

    /// <summary>
    /// Renders a line between two points
    /// </summary>
    public void RenderLine(float x1, float y1, float x2, float y2, Color color)
    {
        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A);
        SDL.RenderLine(Handle, x1, y1, x2, y2);
    }

    /// <summary>
    /// Renders a line between two points
    /// </summary>
    public void RenderLine(Vector2 startPosition, Vector2 endPosition, Color color)
    {
        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A);
        SDL.RenderLine(Handle, startPosition.X, startPosition.Y, endPosition.X, endPosition.Y);
    }

    /// <summary>
    /// Renders a point at the specified position
    /// </summary>
    public void RenderPoint(Vector2 position, Color color)
    {
        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A);
        SDL.RenderPoint(Handle, position.X, position.Y);
    }

    /// <summary>
    /// Renders a point at the specified position
    /// </summary>
    public void RenderPoint(float x, float y, Color color)
    {
        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A);
        SDL.RenderPoint(Handle, x, y);
    }

    /// <summary>
    /// Renders a non-filled rectangle
    /// </summary>
    public void RenderRectangle(Rectangle rectangle, Color color)
    {
        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A);
        SDL.FRect rect = rectangle.ToSDLFRect();

        SDL.RenderRect(Handle, rect);
    }

    /// <summary>
    /// Renders a filled rectangle with the specified color
    /// </summary>
    public void RenderFilledRectangle(Rectangle rectangle, Color color)
    {
        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A);

        SDL.FRect rect = rectangle.ToSDLFRect();

        SDL.RenderFillRect(Handle, rect);
    }

    /// <summary>
    /// Renders a string to the screen at the specified position
    /// </summary>
    public void RenderString(Font font, float pointSize, string text, Vector2 position, Color color)
    {
        FontSize fontSize = font.GetOrCreateFontSize(pointSize);
        fontSize.RenderString(_textEngine, text, position, color);
    }

    /// <summary>
    /// Renders text to the screen at the specified position without the need of a font.
    /// This should mainly be used for debugging purposes
    /// </summary>
    public void RenderDebugText(Vector2 position, string text, Color color)
    {
        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A);
        SDL.RenderDebugText(Handle, position.X, position.Y, text);
    }

    /// <summary>
    /// Renders text to the screen at the specified position without the need of a font.
    /// This should mainly be used for debugging purposes
    /// </summary>
    public void RenderDebugText(float x, float y, string text, Color color)
    {
        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A);
        SDL.RenderDebugText(Handle, x, y, text);
    }

    /// <summary>
    /// Sets the blend mode lol
    /// </summary>
    public void SetRenderBlendMode(BlendMode blendMode)
    {
        SDL.SetRenderDrawBlendMode(Handle, (SDL.BlendMode)blendMode);
    }

    /// <summary>
    /// Enables or disables vsync for this renderer
    /// </summary>
    /// <param name="enabled">If vsync should be enabled or not</param>
    public void SetVSyncEnabled(bool enabled)
    {
        SDL.SetRenderVSync(Handle, enabled ? 1 : 0);
    }

    /// <summary>
    /// Renders everything to the screen
    /// </summary>
    public void RenderPresent()
    {
        SDL.RenderPresent(Handle);
    }

    public void Dispose()
    {
        _textEngine.Dispose();
        SDL.DestroyRenderer(Handle);
    }
}
