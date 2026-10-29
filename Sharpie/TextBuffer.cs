// TextBuffer.cs
namespace Sharpie;

public class TextBuffer
{
  public List<string> Lines { get; set; } = new() { "" };
  public int CursorY { get; set; } = 0;
  public int CursorX { get; set; } = 0;

  public void InsertEnter()
  {
    string currentLine = Lines[CursorY];
    string leftSide = currentLine.Substring(0, CursorX);
    string rightSide = currentLine.Substring(CursorX);

    Lines[CursorY] = leftSide;
    Lines.Insert(CursorY + 1, rightSide);
    CursorY++;
    CursorX = 0;
  }

  public void InsertChar(string input)
  {
    Lines[CursorY] = Lines[CursorY].Insert(CursorX, input);
    CursorX++;
  }

  public void Backspace()
  {
    int indexToRemove = CursorX - 1;

    if (indexToRemove >= 0)
    {
      Lines[CursorY] = Lines[CursorY].Remove(indexToRemove, 1);
      CursorX--;
    }
  }

  public void MoveCursorLeft()
  {
    bool positionGreaterThanZero = CursorX > 0;

    if (positionGreaterThanZero)
      CursorX--;
  }

  public void MoveCursorRight()
  {
    int lineLength = Lines[CursorY].Length;
    bool atLastPosition = CursorX == lineLength - 1;

    if (!atLastPosition)
      CursorX++;
  }

  public void MoveCursorDown()
  {
    bool atLastPosition = CursorY == Lines.Count - 1;

    if (!atLastPosition)
      CursorY++;
  }

  public void MoveCursorUp()
  {
    bool atFirstPosition = CursorY == 0;

    if (!atFirstPosition)
      CursorY--;
  }
}
