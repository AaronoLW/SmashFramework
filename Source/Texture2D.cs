using System.Drawing;
using System.Numerics;
using SDL3;

namespace SmashFramework;

public class Texture2D : IDisposable, IAsset
{
    public float Width { get; private set; }
    public float Height { get; private set; }
    public Vector2 Size => new(Width, Height);

    public readonly nint Handle;

    internal readonly SDL.FRect _sourceRectangle;
    public Rectangle SourceRectangle => new(_sourceRectangle);

    internal Color _modulatedColor;

    public Texture2D(nint textureHandle)
    {
        Handle = textureHandle;
        SDL.GetTextureSize(Handle, out float width, out float height);
        Width = width;
        Height = height;
        _sourceRectangle = new SDL.FRect { X = 0, Y = 0, W = width, H = height };
    }

    internal Texture2D(nint textureHandle, Rectangle sourceRectangle)
    {
        Handle = textureHandle;
        Width = sourceRectangle.Width;
        Height = sourceRectangle.Height;
        _sourceRectangle = sourceRectangle.ToSDLFRect();
    }

    public void Dispose()
    {
        SDL.DestroyTexture(Handle);
    }
}
