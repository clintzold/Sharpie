// Renderer.cs
namespace Sharpie;

public class Renderer(TextBuffer _buffer)
{
  public void Render()
  {
    Console.Clear();

    foreach (var line in _buffer.Lines)
    {
      Console.WriteLine(line);
    }

    Console.SetCursorPosition(_buffer.CursorX, _buffer.CursorY);
  }
}
