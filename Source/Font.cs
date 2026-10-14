using System.Numerics;
using SDL3;

namespace SmashFramework;

public class Font : IDisposable, IAsset
{
    internal string _fontPath;
    internal Dictionary<float, (nint handle, Dictionary<string, nint> texts)> _alreadyCreatedTexts = new();

    public Font(string fontPath)
    {
        _fontPath = fontPath;
    }

    public Vector2 MeasureString(string text, float pointSize)
    {
        TTF.GetTextSize(GetOrCreateText(text, pointSize), out int widht, out int height);
        return new Vector2(widht, height);
    }

    internal nint GetOrCreateText(string text, float pointSize)
    {
        return 0;
    }

    public void Dispose()
    {
        foreach (var idklol in _alreadyCreatedTexts.Values)
        {
            foreach (nint alreadyCreatedText in idklol.texts.Values)
            {
                TTF.DestroyText(alreadyCreatedText);
            }

            TTF.CloseFont(idklol.handle);
        }

        _alreadyCreatedTexts.Clear();
    }
}
