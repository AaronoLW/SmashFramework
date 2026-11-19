# SmashFramework

A very simple framework for making games/graphical applications built on top of SDL3

## Adding to your project
The SmashFramework is available on NuGet and can be installed using:
```
dotnet add package SmashFramework
```

## Examples
This example creates a basic window and renders a red rectangle in the middle of it
```
using SDL3;
using SmashFramework;

Smash.Init();

Window window = new("SmashFramework example", 800, 600, WindowFlag.Resizable);
Renderer renderer = new(window);

bool running = true;
while (running)
{
    while (SDL.PollEvent(out SDL.Event e))
    {
        if (e.Type == (uint)SDL.EventType.Quit)
        {
            running = false;
        }
    }

    renderer.Clear(Color.CornflowerBlue);

    float rectangleWidth = 100;
    float rectangleHeight = 100;

    Rectangle rectangle = new(window.Width / 2 - rectangleWidth / 2, window.Height / 2 - rectangleHeight / 2, rectangleWidth, rectangleHeight);
    renderer.RenderFilledRectangle(rectangle, Color.Red);

    renderer.Present();
}
```
