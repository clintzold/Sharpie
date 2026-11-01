// FrameBuilder.cs
using System.Text;

namespace Sharpie;

public class FrameBuilder(TextBuffer _buffer, int _frameWidth, int _frameHeight)
{
  private readonly StringBuilder _sb = new(4096);
  private readonly string _frameColor = "\x1b[34m";
  private readonly string _title = "SHARPIE";
  private int ScrollOffset { get; set; } = 0;

  public string BuildFrame()
  {
    Reset();

    UpdateScrollOffset();

    AppendHeader();

    AppendBody(_buffer.Lines);

    AppendFooter();

    PositionCursor(_buffer.CursorX, _buffer.CursorY);

    return _sb.ToString();
  }

  private void UpdateScrollOffset()
  {
    int bottomPadding = 1;

    if (_buffer.CursorY < ScrollOffset)
    {
      ScrollOffset = _buffer.CursorY;
    }
    else if (_buffer.CursorY >= ScrollOffset + _frameHeight - bottomPadding)
    {
      ScrollOffset = _buffer.CursorY + bottomPadding + -_frameHeight;
    }
  }
  private void PositionCursor(int x, int y)
  {
    int horizontalOffset = 3;
    int verticalOffset = 4;
    int absoluteX = x + horizontalOffset;
    int absoluteY = y + verticalOffset - ScrollOffset;

    _sb.Append($"\x1b[{absoluteY};{absoluteX}H");
    // Show cursor
    _sb.Append("\x1b[?25h");
  }

  private void Reset()
  {
    _sb.Clear();
    _sb.Append("\x1b[?25l");  // Hide cursor
    _sb.Append("\x1b[H");     // Return cursor to start
  }

  private void AppendHeader()
  {
    // Rounded corner and top border
    _sb.Append(
        _frameColor
        + "\u256D"
        + new string('\u2500', _frameWidth - 2)
        + "\u256E\r\n"
        );

    // Title line
    string leftPadding = new(' ', (_frameWidth / 2) - (_title.Length / 2) - 1);
    string rightPadding = (_title.Length % 2 == 0) ? leftPadding : leftPadding[..(leftPadding.Length - 1)];
    _sb.Append(
        "\u2502"
        + leftPadding
        + $"\x1b[4m{_title}\x1b[0m"
        + rightPadding
        + "\u2502\r\n"
        );

    // Title bottom padding
    _sb.Append(
        "\u2502"
        + new string(' ', _frameWidth - 2)
        + "\u2502\r\n"
        );
  }

  private void AppendBody(List<string> list)
  {
    string padding = " ";
    int paddingTotal = (padding + padding).Length;

    for (int y = 0; y < _frameHeight; y++)
    {
      _sb.Append(_frameColor + "\u2502" + padding);

      if ((y + ScrollOffset) < list.Count)
      {
        string currentLine = list[y + ScrollOffset];

        if (currentLine.Length > _frameWidth - paddingTotal)
        {
          _sb.Append(currentLine[..(_frameWidth - paddingTotal)]);
        }
        else
        {
          _sb.Append(currentLine);
        }
        _sb.Append(new string(' ', _frameWidth - (currentLine.Length + 4)) + padding + "\u2502\r\n");
      }
      else
      {
        _sb.Append(new string(' ', _frameWidth - 3) + "\u2502\r\n");
      }

    }
  }

  private void AppendFooter()
  {
    // Middle Separators
    _sb.Append(
        _frameColor
        + "\u251C"
        + new string('\u2500', _frameWidth - 2)
        + "\u2524\r\n"
        );

    // Title line
    string footerString = "Esc => Quit";
    string leftPadding = new(' ', (_frameWidth / 2) - (footerString.Length / 2) - 1);
    string rightPadding = (footerString.Length % 2 == 0) ? leftPadding : leftPadding[..(leftPadding.Length - 1)];

    _sb.Append(
        "\u2502"
        + leftPadding
        + footerString
        + rightPadding
        + "\u2502\r\n"
        );

    // Bottom corners and border
    _sb.Append(
        _frameColor
        + "\u2570"
        + new string('\u2500', _frameWidth - 2)
        + "\u256F\r\n"
        );
  }
}
