using System.Numerics;
using SDL3;

namespace SmashFramework;

public class Rectangle
{
    public float X;
    public float Y;
    public Vector2 Position
    {
        get { return new Vector2(X, Y); }
        set { X = value.X; Y = value.Y; }
    }

    public float Width;
    public float Height;
    public Vector2 Size
    {
        get { return new Vector2(Width, Height); }
        set { Width = value.X; Height = value.Y; }
    }

    public Rectangle(Vector2 position, float width, float height)
    {
        Position = position;
        Width = width;
        Height = height;
    }

    public Rectangle(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public Rectangle(Vector2 position, Vector2 size)
    {
        Position = position;
        Size = size;
    }

    public Rectangle(SDL.FRect sdlRect)
    {
        X = sdlRect.X;
        Y = sdlRect.Y;
        Width = sdlRect.W;
        Height = sdlRect.H;
    }

    public Rectangle() { }


    public SDL.FRect ToSDLFRect()
    {
        return new SDL.FRect
        {
            X = X,
            Y = Y,
            W = Width,
            H = Height
        };
    }

    public SDL.Rect ToSDLRect()
    {
        return new SDL.Rect
        {
            X = (int)X,
            Y = (int)Y,
            W = (int)Width,
            H = (int)Height
        };
    }

    public bool IsPositionInRectangle(Vector2 position)
    {
        return position.X >= X &&
               position.X <= X + Width &&
               position.Y >= Y &&
               position.Y <= Y + Height;
    }

    public bool IntersectsWith(Rectangle otherRectangle)
    {
        return X < otherRectangle.X + otherRectangle.Width &&
               X + Width > otherRectangle.X &&
               Y < otherRectangle.Y + otherRectangle.Height &&
               Y + Height > otherRectangle.Y;
    }

    public Vector2 GetRandomPositionInRectangle(Random random)
    {
        return new Vector2(random.Next((int)X, (int)(X + Width)), random.Next((int)Y, (int)(Y + Height)));
    }

    public Rectangle Clone()
    {
        return new Rectangle
        {
            X = X,
            Y = Y,
            Width = Width,
            Height = Height,
        };
    }
}
