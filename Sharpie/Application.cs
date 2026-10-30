// Application.cs
namespace Sharpie;

public class Application
{
  public void Run()
  {
    TextBuffer textBuffer = new();
    KeyboardInput keyboardInput = new();

    KeyboardListener listener = new(keyboardInput, textBuffer);
    Renderer renderer = new(textBuffer);

    while (true)
    {
      listener.Listen();

      renderer.Render();
    }
  }
}
