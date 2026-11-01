// FrameBuilder.cs
using System.Text;

namespace Sharpie;

public class FrameBuilder
{
  private readonly StringBuilder _sb = new(4096);
  private readonly string _frameColor = "\x1b[34m";
  private readonly string _title = "SHARPIE";
  private readonly int bodyPadding = 4;

  public string BuildFrame(TextBuffer buffer, int frameWidth, int frameHeight)
  {

    Reset();

    AppendHeader(frameWidth);

    AppendBody(buffer.Lines, frameWidth, frameHeight);

    AppendFooter(frameWidth);

    PositionCursor(buffer.CursorX, buffer.CursorY);

    // Show cursor
    _sb.Append("\x1b[?25h");

    return _sb.ToString();
  }

  private void PositionCursor(int x, int y)
  {
    int absoluteX = x + bodyPadding - 1;
    int absoluteY = y + bodyPadding;

    _sb.Append($"\x1b[{absoluteY};{absoluteX}H");

  }

  private void Reset()
  {
    _sb.Clear();
    _sb.Append("\x1b[?25l");  // Hide cursor
    _sb.Append("\x1b[H");     // Return cursor to start
  }

  private void AppendHeader(int width)
  {
    // Rounded corner and top border
    _sb.Append(
        _frameColor
        + "\u256D"
        + new string('\u2500', width - 2)
        + "\u256E\r\n"
        );

    // Title line
    string xPadding = new(' ', (width / 2) - (_title.Length / 2) - 1);
    _sb.Append(
        "\u2502"
        + xPadding
        + $"\x1b[4m{_title}\x1b[0m"
        + xPadding
        + "\u2502\r\n"
        );

    // Title bottom padding
    _sb.Append(
        "\u2502"
        + new string(' ', width - 2)
        + "\u2502\r\n"
        );
  }

  private void AppendBody(List<string> list, int width, int editFrameHeight)
  {
    string padding = " ";
    int paddingTotal = (padding + padding).Length;

    for (int dynamicY = 0; dynamicY < editFrameHeight; dynamicY++)
    {
      _sb.Append(_frameColor + "\u2502" + padding);

      if (dynamicY < list.Count)
      {
        string currentLine = list[dynamicY];

        if (currentLine.Length > width - paddingTotal)
        {
          _sb.Append(currentLine[..(width - paddingTotal)]);
        }
        else
        {
          _sb.Append(currentLine);
        }
        _sb.Append(new string(' ', width - (currentLine.Length + 4)) + padding + "\u2502\r\n");
      }
      else
      {
        _sb.Append(new string(' ', width - 3) + "\u2502\r\n");
      }
    }
  }

  private void AppendFooter(int width)
  {
    // Middle Separators
    _sb.Append(
        _frameColor
        + "\u251C"
        + new string('\u2500', width - 2)
        + "\u2524\r\n"
        );

    // Title line
    string footerString = "Esc => Quit";
    string xPadding = new(' ', (width / 2) - (footerString.Length / 2) - 1);
    _sb.Append(
        "\u2502"
        + xPadding
        + footerString
        + xPadding
        + "\u2502\r\n"
        );

    // Bottom corners and border
    _sb.Append(
        _frameColor
        + "\u2570"
        + new string('\u2500', width - 2)
        + "\u256F\r\n"
        );
  }

}
