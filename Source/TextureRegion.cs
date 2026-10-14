using System.Numerics;

namespace SmashFramework;

public class TextureRegion(string baseTextureName, int x, int y, int width, int height)
{
    public readonly string BaseTextureName = baseTextureName;

    public readonly int X = x;
    public readonly int Y = y;
    public Vector2 Position => new(X, Y);

    public readonly int Width = width;
    public readonly int Height = height;
    public Vector2 Size => new(Width, Height);
}
