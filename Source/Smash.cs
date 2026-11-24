using SDL3;

namespace SmashFramework;

public static class Smash
{
    /// <summary>
    /// Gets the current value of the hight resolution counter.
    /// </summary>
    public static ulong PerformanceCounter => SDL.GetPerformanceCounter();

    /// <summary>
    /// Gets the count per second of the high resolution counter.
    /// </summary>
    public static ulong PerformanceFrequency => SDL.GetPerformanceFrequency();

    public static void Init()
    {
        if (!SDL.Init(SDL.InitFlags.Video))
            throw new Exception($"SDL couldn't initialize: {SDL.GetError()}");

        if (!TTF.Init())
            throw new Exception($"SDL TTF couldn't initialize: {SDL.GetError()}");
    }

    public static void Quit()
    {
        SDL.Quit();
        TTF.Quit();
    }
}
