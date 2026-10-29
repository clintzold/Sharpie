// KeyboardListener.cs
namespace Sharpie;

public class KeyboardListener(IKeyboardInput input, TextBuffer buffer)
{
  public int EnterCount { get; private set; }

  public void Listen()
  {
    while (true)
    {
      ConsoleKeyInfo keyInfo = input.ReadKey();

      if (keyInfo.Key == ConsoleKey.Escape)
      {
        break;
      }
      ProcessKey(keyInfo);
    }
  }

  private void ProcessKey(ConsoleKeyInfo keyInfo)
  {
    bool isModifierKey = keyInfo.KeyChar == '\0';

    if (isModifierKey)
    {
      switch (keyInfo.Key)
      {
        case ConsoleKey.Enter:
          EnterCount++;
          buffer.InsertEnter();
          break;
        case ConsoleKey.Backspace:
          buffer.Backspace();
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
