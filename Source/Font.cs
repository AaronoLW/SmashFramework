using SDL3;

public class Font : IDisposable
{
    public readonly string Path;
    public float PointSize
    {
        get => TTF.GetFontSize(_fontHandle);
        set { SetFontSize(value); }
    }

    internal nint _fontHandle { get; private set; }

    public Font(string path, float initialPointSize)
    {
        Path = path;
        _fontHandle = TTF.OpenFont(path, initialPointSize);
    }

    private void SetFontSize(float pointSize)
    {
        if (pointSize != PointSize)
        {
            TTF.CloseFont(_fontHandle);
            _fontHandle = TTF.OpenFont(Path, pointSize);
        }
    }

    public void Dispose()
    {
        TTF.CloseFont(_fontHandle);
    }
}
