using System.Numerics;

namespace SmashFramework;

public class Font : IDisposable, IAsset
{
    public readonly string Path;
    private readonly Dictionary<float, FontSize> _fontSizes = [];

    private readonly TextEngine _textEngine;

    internal Font(string fontPath, TextEngine textEngine)
    {
        Path = fontPath;
        _textEngine = textEngine;
    }

    internal FontSize GetOrCreateFontSize(float pointSize)
    {
        if (_fontSizes.TryGetValue(pointSize, out FontSize? alreadyExistingFontSize))
        {
            return alreadyExistingFontSize;
        }

        _fontSizes.Add(pointSize, new(Path, pointSize));
        return _fontSizes[pointSize];
    }

    public Vector2 MeasureString(string text, float pointSize)
    {
        FontSize fontSize = GetOrCreateFontSize(pointSize);
        return fontSize.MeasureString(text, _textEngine);
    }

    public void DeleteSizeIfExists(float pointSize)
    {
        if (_fontSizes.ContainsKey(pointSize))
        {
            _fontSizes[pointSize].Dispose();
            _fontSizes.Remove(pointSize);
        }
    }

    public void Dispose()
    {
        foreach (FontSize fontSize in _fontSizes.Values)
            fontSize.Dispose();

        _fontSizes.Clear();
    }
}
