using SDL3;

namespace SmashFramework;

public static class AssetManager
{
    internal static readonly List<IAsset> LoadedAssets = [];
    internal static Dictionary<string, int> _aliases = [];

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
        if (!_aliases.TryGetValue(assetName, out int id))
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

    public static void Remove(string assetName)
    {
        if (_aliases.TryGetValue(assetName, out int index))
        {
            LoadedAssets[index].Dispose();
            LoadedAssets.RemoveAt(index);
            _aliases.Remove(assetName);
        }
    }

    public static int GetAssetId(string assetName)
    {
        if (_aliases.TryGetValue(assetName, out int id))
        {
            return id;
        }

        throw new Exception($"{assetName} could not be found");
    }

    /// <summary>
    /// Gets the assets name by its id
    /// </summary>
    public static string GetAssetName(int id)
    {
        return _aliases.FirstOrDefault(x => x.Value == id).Key;
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
    /// Loads a Texture2D from an image
    /// </summary>
    public static Texture2D LoadTexture(string identifier, string path, Renderer renderer)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"File at {path} could not be found");

        FileStream fs = File.OpenRead(path);
        return LoadTexture(identifier, fs, renderer);
    }

    public static Texture2D LoadTexture(string identifier, Stream stream, Renderer renderer)
    {
        SDL.IOStreamOwner sdlStream = SDL.IOFromStream(stream);

        nint textureHandle = Image.LoadTextureIO(renderer.Handle, sdlStream.Handle, true);
        Texture2D texture = new(textureHandle);

        SDL.SetTextureBlendMode(texture.Handle, (SDL.BlendMode)_defaultBlendMode);
        SDL.SetTextureScaleMode(texture.Handle, (SDL.ScaleMode)_defaultScaleMode);

        _aliases.Add(identifier, LoadedAssets.Count);
        LoadedAssets.Add(texture);
        return texture;
    }

    /// <summary>
    /// Adds a texture region to a texture. This texture can be retrieved by calling AssetManager.Get<Texture2D>(name);
    /// </summary>
    public static Texture2D AddTextureRegion(string name, TextureRegion textureRegion)
    {
        if (!_aliases.TryGetValue(textureRegion.BaseTextureName, out int baseTextureId))
            throw new Exception($"""Base texture "{textureRegion.BaseTextureName}" could not be found""");

        Texture2D? baseTexture = LoadedAssets[baseTextureId] as Texture2D;
        if (baseTexture == null) throw new Exception($"""Base texture {textureRegion.BaseTextureName} doesn't exist""");

        Texture2D texture = new(baseTexture.Handle, new Rectangle(textureRegion.X, textureRegion.Y, textureRegion.Width, textureRegion.Height));
        _aliases.Add(name, LoadedAssets.Count);
        LoadedAssets.Add(texture);

        return texture;
    }

    public static Font LoadFont(string identifier, string fontPath, Renderer renderer)
    {
        if (!File.Exists(fontPath))
            throw new FileNotFoundException($"Font at {fontPath} doesn't exist");

        FileStream fs = File.OpenRead(fontPath);
        return LoadFont(identifier, fs, renderer);
    }

    public static Font LoadFont(string identifier, Stream stream, Renderer renderer)
    {
        Font font = new(stream, renderer._textEngine);

        _aliases.Add(identifier, LoadedAssets.Count);
        LoadedAssets.Add(font);

        return (Font)LoadedAssets[_aliases[identifier]];
    }

    /// <summary>
    /// Disposes all assets
    /// </summary>
    public static void Dispose()
    {
        foreach (IAsset asset in LoadedAssets)
        {
            asset.Dispose();
        }
    }
}
