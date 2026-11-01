using System.Numerics;
using SDL3;

namespace SmashFramework;

public class Font : IDisposable, IAsset
{
    private readonly Dictionary<float, FontSize> _fontSizes = [];

    private readonly SDL.IOStreamOwner _stream;
    private readonly TextEngine _textEngine;

    internal Font(Stream stream, TextEngine textEngine)
    {
        _stream = SDL.IOFromStream(stream);
        _textEngine = textEngine;
    }

    internal FontSize GetOrCreateFontSize(float pointSize)
    {
        if (_fontSizes.TryGetValue(pointSize, out FontSize? alreadyExistingFontSize))
        {
            return alreadyExistingFontSize;
        }

        _fontSizes.Add(pointSize, new(_stream.Handle, pointSize));
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
        _stream.Dispose();
    }
}
