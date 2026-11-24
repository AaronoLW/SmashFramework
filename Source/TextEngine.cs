using SDL3;
using SmashFramework;

namespace SmashFramework;

internal class TextEngine(Renderer renderer) : IDisposable
{
    internal readonly nint _handle = TTF.CreateRendererTextEngine(renderer.Handle);

    public void Dispose()
    {
        TTF.DestroyRendererTextEngine(_handle);
    }
}
