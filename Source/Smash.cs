using SDL3;

namespace SmashFramework;

public static class Smash
{
    public static void Init()
    {
        if (!SDL.Init(SDL.InitFlags.Video))
        {
            throw new Exception($"SDL couldn't initialize: {SDL.GetError()}");
        }

        if (!TTF.Init())
        {
            throw new Exception($"SDL TTF couldn't initialize: {SDL.GetError()}");
        }
    }

    public static void Quit()
    {
        SDL.Quit();
        TTF.Quit();
    }
}
