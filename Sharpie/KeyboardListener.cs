// KeyboardListener.cs
namespace Sharpie;

public class KeyboardListener(IKeyboardInput input, TextBuffer buffer)
{
  private static readonly char[] _specialKeys = [
    '\0',
    '\r',
    '\b',
    '\x1b'
  ];
  public bool IsRunning { get; private set; } = true;

  public void Listen()
  {
    ConsoleKeyInfo keyInfo = input.ReadKey();

    ProcessKey(keyInfo);
  }

  private void ProcessKey(ConsoleKeyInfo keyInfo)
  {
    bool isSpecialKey = Array.Exists(_specialKeys, k => k == keyInfo.KeyChar);

    if (isSpecialKey)
    {
      switch (keyInfo.Key)
      {
        case ConsoleKey.Escape:
          IsRunning = false;
          break;
        case ConsoleKey.Enter:
          buffer.InsertEnter();
          break;
        case ConsoleKey.Backspace:
          buffer.Backspace();
          break;
        case ConsoleKey.UpArrow:
          buffer.MoveCursorUp();
          break;
        case ConsoleKey.DownArrow:
          buffer.MoveCursorDown();
          break;
        case ConsoleKey.LeftArrow:
          buffer.MoveCursorLeft();
          break;
        case ConsoleKey.RightArrow:
          buffer.MoveCursorRight();
          break;
        default:
          break;
      }
    }
    else
    {
      string convertedChar = keyInfo.KeyChar.ToString();
      buffer.InsertChar(convertedChar);
    }
  }
}
