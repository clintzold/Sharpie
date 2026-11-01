// Renderer.cs
namespace Sharpie;

public class Renderer(TextBuffer _buffer)
{
  private readonly FrameBuilder _frameBuilder = new();

  public void Render()
  {
    string text = _frameBuilder.BuildFrame(_buffer, Console.WindowWidth, Console.WindowHeight - 7);

    Console.Write(text);
  }

}
