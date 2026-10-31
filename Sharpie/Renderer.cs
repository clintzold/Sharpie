// Renderer.cs
using System.Text;

namespace Sharpie;

public class Renderer(TextBuffer _buffer)
{
  private readonly StringBuilder _stringBuilder = new(4096);

  public void Render()
  {
    _stringBuilder.Clear();

    // Clear the screen
    Console.Write("\x1b[2J");

    // Position the cursor for writing the string
    Console.SetCursorPosition(0, 0);

    // Build the string from enumerable List
    for (int y = 0; y < _buffer.Lines.Count; y++)
    {
      string currentLine = _buffer.Lines[y];

      for (int x = 0; x < currentLine.Length; x++)
      {
        _stringBuilder.Append(currentLine[x]);
      }
      _stringBuilder.AppendLine();
    }

    Console.Write(_stringBuilder.ToString());

    Console.SetCursorPosition(_buffer.CursorX, _buffer.CursorY);
  }
}
