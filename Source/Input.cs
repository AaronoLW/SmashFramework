using System.Numerics;
using SDL3;

namespace SmashFramework;

public static class Input
{
    public static float MouseX { get; private set; }
    public static float MouseY { get; private set; }
    public static Vector2 MousePosition => new Vector2(MouseX, MouseY);

    public static float ScrollWheelDelta { get; private set; }

    private static HashSet<SDL.Keycode> _previousDownKeys = new();
    private static HashSet<SDL.Keycode> _previousPressedKeys = new();

    private static HashSet<SDL.Keycode> _downKeys = new();
    private static HashSet<SDL.Keycode> _pressedKeys = new();

    private static bool _leftMouseDown;
    private static bool _rightMouseDown;
    private static bool _middleMouseDown;

    private static bool _leftMousePressed;
    private static bool _rightMousePressed;
    private static bool _middleMousePressed;

    public static void Event(SDL.Event e)
    {
        _previousDownKeys = _downKeys;
        _previousPressedKeys = _pressedKeys;

        if (e.Type == (uint)SDL.EventType.KeyDown && !e.Key.Repeat)
        {
            _downKeys.Add(e.Key.Key);
            _pressedKeys.Add(e.Key.Key);
        }
        else if (e.Type == (uint)SDL.EventType.KeyUp)
        {
            _downKeys.Remove(e.Key.Key);
        }

        if (e.Type == (uint)SDL.EventType.MouseButtonDown)
        {
            if (e.Button.Button == SDL.ButtonLeft)
            {
                _leftMousePressed = !_leftMouseDown;
                _leftMouseDown = true;
            }

            if (e.Button.Button == SDL.ButtonRight)
            {
                _rightMousePressed = !_rightMouseDown;
                _rightMouseDown = true;
            }

            if (e.Button.Button == SDL.ButtonMiddle)
            {
                _middleMousePressed = !_middleMouseDown;
                _middleMouseDown = true;
            }
        }

        if (e.Type == (uint)SDL.EventType.MouseButtonUp)
        {
            if (e.Button.Button == SDL.ButtonLeft)
            {
                _leftMouseDown = false;
            }

            if (e.Button.Button == SDL.ButtonRight)
            {
                _rightMouseDown = false;
            }

            if (e.Button.Button == SDL.ButtonMiddle)
            {
                _middleMouseDown = false;
            }
        }

        if (e.Type == (uint)SDL.EventType.MouseMotion)
        {
            SDL.GetMouseState(out float x, out float y);
            MouseX = x;
            MouseY = y;
        }

        if (e.Type == (uint)SDL.EventType.MouseWheel)
        {
            ScrollWheelDelta = e.Wheel.Y;
        }
    }

    public static void Update()
    {
        _pressedKeys.Clear();
        _leftMousePressed = false;
        _rightMousePressed = false;
        _middleMousePressed = false;
        ScrollWheelDelta = 0;
    }

    public static bool IsKeyDown(SDL.Keycode key) => _downKeys.Contains(key);
    public static bool IsKeyPressed(SDL.Keycode key) => _pressedKeys.Contains(key);

    public static bool WasKeyDown(SDL.Keycode key) => _previousDownKeys.Contains(key);
    public static bool WasKeyPressed(SDL.Keycode key) => _previousPressedKeys.Contains(key);

    public static bool IsLeftMouseDown() => _leftMouseDown;
    public static bool IsRightMouseDown() => _rightMouseDown;
    public static bool IsMiddleMouseDown() => _middleMouseDown;

    public static bool IsLeftMousePressed() => _leftMousePressed;
    public static bool IsRightMousePressed() => _rightMousePressed;
    public static bool IsMiddleMousePressed() => _middleMousePressed;
}
