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

    // Switch to alternate screen buffer
    Console.Write("\x1b[?1049h");

    while (listener.IsRunning)
    {
      renderer.Render();

      listener.Listen();
    }

    // Restore main screen buffer on exit
    Console.Write("\x1b[?1049l;");
  }
}
