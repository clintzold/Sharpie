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
    Console.Write("\x1b[H");

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

    Console.Write($"\x1b[{_buffer.CursorY + 1};{_buffer.CursorX + 1}H");
  }
}
