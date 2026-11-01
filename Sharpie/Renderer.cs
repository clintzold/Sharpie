// Renderer.cs
namespace Sharpie;

public class Renderer(TextBuffer _buffer)
{
  private readonly FrameBuilder _frameBuilder = new(_buffer, Console.WindowWidth, Console.WindowHeight - 7);

  public void Render()
  {
    string text = _frameBuilder.BuildFrame();

    Console.Write(text);
  }

}
