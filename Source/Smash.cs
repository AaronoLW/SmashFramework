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

    public static bool Init()
    {
        try
        {
            if (!SDL.Init(SDL.InitFlags.Video))
                throw new Exception($"SDL couldn't initialize: {SDL.GetError()}");

            if (!TTF.Init())
                throw new Exception($"SDL TTF couldn't initialize: {SDL.GetError()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

        return true;
    }

    public static void Quit()
    {
        SDL.Quit();
        TTF.Quit();
    }
}
