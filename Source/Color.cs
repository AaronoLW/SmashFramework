namespace SmashFramework;

public struct Color
{
    public byte R;
    public byte G;
    public byte B;
    public byte A;

    public Color(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
        A = 255;
    }

    public Color(byte r, byte g, byte b, byte a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public static readonly Color White = new(255, 255, 255, 255);
    public static readonly Color Black = new(0, 0, 0, 255);

    public static readonly Color Red = new(255, 0, 0, 255);
    public static readonly Color Green = new(0, 255, 0, 255);
    public static readonly Color Blue = new(0, 0, 255, 255);

    public static readonly Color Yellow = new(255, 255, 0, 255);
    public static readonly Color Cyan = new(0, 255, 255, 255);
    public static readonly Color Magenta = new(255, 0, 255, 255);

    public static readonly Color Orange = new(255, 165, 0, 255);
    public static readonly Color Purple = new(128, 0, 128, 255);
    public static readonly Color Pink = new(255, 192, 203, 255);
    public static readonly Color Brown = new(165, 42, 42, 255);

    public static readonly Color Gray = new(128, 128, 128, 255);
    public static readonly Color LightGray = new(211, 211, 211, 255);
    public static readonly Color DarkGray = new(64, 64, 64, 255);

    public static readonly Color Lime = new(0, 255, 0, 255);
    public static readonly Color Navy = new(0, 0, 128, 255);
    public static readonly Color Teal = new(0, 128, 128, 255);
    public static readonly Color Olive = new(128, 128, 0, 255);
    public static readonly Color Maroon = new(128, 0, 0, 255);

    public static readonly Color Gold = new(255, 215, 0, 255);
    public static readonly Color Silver = new(192, 192, 192, 255);
    public static readonly Color Coral = new(255, 127, 80, 255);
    public static readonly Color Salmon = new(250, 128, 114, 255);
    public static readonly Color Indigo = new(75, 0, 130, 255);
    public static readonly Color Violet = new(238, 130, 238, 255);
    public static readonly Color SkyBlue = new(135, 206, 235, 255);
    public static readonly Color CornflowerBlue = new(100, 149, 237, 255);
    public static readonly Color Turquoise = new(64, 224, 208, 255);
    public static readonly Color Beige = new(245, 245, 220, 255);
    public static readonly Color Chocolate = new(210, 105, 30, 255);

    public static Color FromHex(string hex)
    {
        hex = hex.TrimStart('#');

        if (hex.Length == 6)
        {
            return new(
                Convert.ToByte(hex.Substring(0, 2), 16),
                Convert.ToByte(hex.Substring(2, 2), 16),
                Convert.ToByte(hex.Substring(4, 2), 16),
                255
            );
        }
        else if (hex.Length == 8)
        {
            return new(
                Convert.ToByte(hex.Substring(0, 2), 16),
                Convert.ToByte(hex.Substring(2, 2), 16),
                Convert.ToByte(hex.Substring(4, 2), 16),
                Convert.ToByte(hex.Substring(6, 2), 16)
            );
        }
        else
        {
            throw new FormatException("Hex string wasn't in the right format, expected: #RRGGBB or #RRGGBBAA");
        }
    }
}
