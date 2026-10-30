// KeyboardListener.cs
namespace Sharpie;

public class KeyboardListener(IKeyboardInput input, TextBuffer buffer)
{
  private static readonly ConsoleKey[] _specialKeys = [
    ConsoleKey.Backspace,
    ConsoleKey.Enter,
    ConsoleKey.Escape,
    ConsoleKey.Tab
  ];

  public bool IsRunning { get; private set; } = true;

  public void Listen()
  {
    ConsoleKeyInfo keyInfo = input.ReadKey();

    ProcessKey(keyInfo);
  }

  private void ProcessKey(ConsoleKeyInfo keyInfo)
  {
    bool isModifierKey = keyInfo.KeyChar == '\0';
    bool isSpecialKey = Array.Exists(_specialKeys, k => k == keyInfo.Key);

    if (isSpecialKey || isModifierKey)
    {
      switch (keyInfo.Key)
      {
        case ConsoleKey.Escape:
          IsRunning = false;
          break;
        case ConsoleKey.Enter:
          buffer.InsertEnter();
          break;
        case ConsoleKey.Tab:
          buffer.InsertChar(" ");
          buffer.InsertChar(" ");
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
