using System.Numerics;

namespace SmashFramework;

public static class MathHelper
{
    public static float Larp(float startValue, float endValue, float time)
    {
        return startValue + (endValue - startValue) * time;
    }

    public static Vector2 LarpVector(Vector2 startValue, Vector2 endValue, float time)
    {
        return startValue + (endValue - startValue) * time;
    }
}
