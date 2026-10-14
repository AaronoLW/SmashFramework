using SDL3;

namespace SmashFramework;

public static class AssetManager
{
    internal static List<IAsset> LoadedAssets { get; } = new();
    internal static Dictionary<string, int> _aliases = new();

    private static string _rootDirectoryPath = "";
    private static BlendMode _defaultBlendMode = BlendMode.Blend;
    private static ScaleMode _defaultScaleMode = ScaleMode.Linear;

    /// <summary>
    /// Gets the asset with the specified name
    /// </summary>
    public static T Get<T>(string assetName) where T : IAsset
    {
        return (T)LoadedAssets[_aliases[assetName]];
    }

    /// <summary>
    /// Tries to get the asset with the specified name
    /// </summary>
    public static T? TryGet<T>(string assetName) where T : IAsset
    {
        int id;

        if (!_aliases.TryGetValue(assetName, out id))
            return default;

        return (T)LoadedAssets[id];
    }

    /// <summary>
    /// Gets the asset with the specified id
    /// </summary>
    public static T Get<T>(int assetId) where T : IAsset
    {
        return (T)LoadedAssets[assetId];
    }

    /// <summary>
    /// Tries to get the asset with the specified id
    /// </summary>
    public static T? TryGet<T>(int assetId) where T : IAsset
    {
        if (LoadedAssets.Count - 1 < assetId) return default;

        return (T)LoadedAssets[assetId];
    }

    public static int GetAssetId(string assetName)
    {
        if (_aliases.TryGetValue(assetName, out int id))
        {
            return id;
        }

        throw new Exception($"{assetName} could not be found");
    }

    public static string GetAssetName(int id)
    {
        return _aliases.FirstOrDefault(x => x.Value == id).Key;
    }

    /// <summary>
    /// Sets the relative path from which all assets will be loaded
    /// </summary>
    /// <param name="directoryPath"></param>
    public static void SetAssetRootDirectory(string directoryPath)
    {
        _rootDirectoryPath = directoryPath;
    }

    /// <summary>
    /// Sets the default blend mode that should be applied to every loaded Texture2D after this function has been called
    /// </summary>
    public static void SetDefaultBlendMode(BlendMode blendMode)
    {
        _defaultBlendMode = blendMode;
    }

    /// <summary>
    /// Sets the default scale mode that should be applied to every loaded Texture2D after this function has been called
    /// </summary>
    public static void SetDefaultScaleMode(ScaleMode scaleMode)
    {
        _defaultScaleMode = scaleMode;
    }

    /// <summary>
    /// Loads a Texture2D from an image relative to the root directory path
    /// </summary>
    public static Texture2D LoadTexture(string relativePath, Renderer renderer)
    {
        string fullPath = Path.Combine(_rootDirectoryPath, relativePath);
        string fileName = Path.GetFileNameWithoutExtension(fullPath);

        if (!File.Exists(fullPath)) throw new FileNotFoundException($"File at {fullPath} could not be found");

        nint textureHandle = Image.LoadTexture(renderer.Handle, fullPath);
        Texture2D texture = new Texture2D(textureHandle, fileName);

        SDL.SetTextureBlendMode(texture.Handle, (SDL.BlendMode)_defaultBlendMode);
        SDL.SetTextureScaleMode(texture.Handle, (SDL.ScaleMode)_defaultScaleMode);

        _aliases.Add(fileName, LoadedAssets.Count);
        LoadedAssets.Add(texture);
        return texture;
    }

    public static Texture2D AddTextureRegion(string name, TextureRegion textureRegion)
    {
        int baseTextureId;

        if (!_aliases.TryGetValue(textureRegion.BaseTextureName, out baseTextureId))
            throw new Exception($"""Base texture "{textureRegion.BaseTextureName}" could not be found""");

        Texture2D? baseTexture = LoadedAssets[baseTextureId] as Texture2D;
        if (baseTexture == null) throw new Exception($"""Base texture {textureRegion.BaseTextureName} doesn't exist""");

        Texture2D texture = new Texture2D(baseTexture.Handle, name, new Rectangle(textureRegion.X, textureRegion.Y, textureRegion.Width, textureRegion.Height));
        _aliases.Add(name, LoadedAssets.Count);
        LoadedAssets.Add(texture);

        return texture;
    }

    /// <summary>
    /// Loads a Font from a .ttf file relative to the root directory path
    /// </summary>
    public static void LoadFont(string relativePath)
    {
        string fullPath = Path.Combine(_rootDirectoryPath, relativePath);
        string fontName = Path.GetFileNameWithoutExtension(fullPath);

        if (!File.Exists(fullPath)) throw new FileNotFoundException($"Font at {fullPath} could not be found");

        Font font = new Font(fullPath);

        _aliases.Add(fontName, LoadedAssets.Count);
        LoadedAssets.Add(font);
    }

    public static void Dispose()
    {
        foreach (IAsset asset in LoadedAssets)
        {
            asset.Dispose();
        }
    }
}
