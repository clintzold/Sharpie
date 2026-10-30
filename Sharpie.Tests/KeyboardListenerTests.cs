using Xunit;
using Sharpie;

namespace Sharpie.Tests;

public class KeyboardListenerTests
{
  // Initialize TextBuffer for injecting into KeyboardListener
  private readonly TextBuffer buffer = new TextBuffer();

  // Define keyboard input class that doesn't need typing or loops
  private class FakeKeyboardInput(IEnumerable<ConsoleKeyInfo> keys) : IKeyboardInput
  {
    // Load the array of mock key presses into the queue
    private readonly Queue<ConsoleKeyInfo> _keys = new(keys);
    public bool HasKeys { get; private set; } = true;

    // Use dequeue instead of manual typing
    public ConsoleKeyInfo ReadKey()
    {
      if (_keys.Count > 0)
      {
        return _keys.Dequeue();
      }

      HasKeys = false;
      return new ConsoleKeyInfo('\0', ConsoleKey.Escape, false, false, false);
    }
  }

  [Fact]
  public void Listen_WhenEscapeKeyIsPressed_IsRunning_BecomesFalse()
  {
    var fakeKeys = new[]
    {
      new ConsoleKeyInfo('\x1b', ConsoleKey.Escape, false, false, false),
    };

    var fakeInput = new FakeKeyboardInput(fakeKeys);
    var listener = new KeyboardListener(fakeInput, buffer);

    listener.Listen();

    Assert.False(listener.IsRunning);
  }

  [Fact]
  public void Listen_WhenLettersArePressed_AddsThemToCurrentLine()
  {
    var fakeKeys = new[]
    {
      new ConsoleKeyInfo('h', ConsoleKey.H, false, false, false),
      new ConsoleKeyInfo('e', ConsoleKey.E, false, false, false),
      new ConsoleKeyInfo('l', ConsoleKey.L, false, false, false),
      new ConsoleKeyInfo('l', ConsoleKey.L, false, false, false),
      new ConsoleKeyInfo('o', ConsoleKey.O, false, false, false)
    };

    var fakeInput = new FakeKeyboardInput(fakeKeys);
    var listener = new KeyboardListener(fakeInput, buffer);

    while (fakeInput.HasKeys)
    {
      listener.Listen();
    }

    Assert.Equal(5, buffer.Lines[0].Length);
  }

  [Fact]
  public void Listen_LettersArePressed_AddsThemToCurrentLine()
  {
    var fakeKeys = new[]
    {
      new ConsoleKeyInfo('h', ConsoleKey.H, false, false, false),
      new ConsoleKeyInfo('e', ConsoleKey.E, false, false, false),
      new ConsoleKeyInfo('l', ConsoleKey.L, false, false, false),
      new ConsoleKeyInfo('l', ConsoleKey.L, false, false, false),
      new ConsoleKeyInfo('o', ConsoleKey.O, false, false, false)
    };

    var fakeInput = new FakeKeyboardInput(fakeKeys);
    var listener = new KeyboardListener(fakeInput, buffer);

    while (fakeInput.HasKeys)
    {
      listener.Listen();
    }

    Assert.Equal(5, buffer.Lines[0].Length);
  }

  [Fact]
  public void Listen_SpaceIsPressed_AddsBlankSpace()
  {
    var fakeKeys = new[]
    {
      new ConsoleKeyInfo('h', ConsoleKey.H, false, false, false),
      new ConsoleKeyInfo('e', ConsoleKey.E, false, false, false),
      new ConsoleKeyInfo('l', ConsoleKey.L, false, false, false),
      new ConsoleKeyInfo('l', ConsoleKey.L, false, false, false),
      new ConsoleKeyInfo('o', ConsoleKey.O, false, false, false),
      new ConsoleKeyInfo(' ', ConsoleKey.O, false, false, false),
      new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false),
      new ConsoleKeyInfo('o', ConsoleKey.O, false, false, false),
      new ConsoleKeyInfo('r', ConsoleKey.R, false, false, false),
      new ConsoleKeyInfo('l', ConsoleKey.L, false, false, false),
      new ConsoleKeyInfo('d', ConsoleKey.D, false, false, false)
    };

    var fakeInput = new FakeKeyboardInput(fakeKeys);
    var listener = new KeyboardListener(fakeInput, buffer);

    while (fakeInput.HasKeys)
    {
      listener.Listen();
    }

    Assert.Equal(11, buffer.Lines[0].Length);
    Assert.Equal("hello world", buffer.Lines[0]);
  }

  [Fact]
  public void Listen_EnterKeyIsPressed_SplitsLineAtCursor()
  {
    var modifierKeys = new[]
    {
      new ConsoleKeyInfo('\r', ConsoleKey.Enter, false, false, false),
    };
    buffer.Lines = ["Hello World"];
    buffer.CursorX = 5;

    var fakeInput = new FakeKeyboardInput(modifierKeys);
    var listener = new KeyboardListener(fakeInput, buffer);

    while (fakeInput.HasKeys)
    {
      listener.Listen();
    }

    Assert.Equal(2, buffer.Lines.Count);
  }

  [Fact]
  public void Listen_MovesCursorIn2DimensionsWhen_ArrowKeysArePressed()
  {
    var arrowKeys = new[]
    {
      new ConsoleKeyInfo('\0', ConsoleKey.DownArrow, false, false, false),
      new ConsoleKeyInfo('\0', ConsoleKey.RightArrow, false, false, false),
      new ConsoleKeyInfo('\0', ConsoleKey.RightArrow, false, false, false),
      new ConsoleKeyInfo('\0', ConsoleKey.UpArrow, false, false, false),
      new ConsoleKeyInfo('\0', ConsoleKey.LeftArrow, false, false, false)
    };
    buffer.Lines = ["First Line", "Second Line"];

    var fakeInput = new FakeKeyboardInput(arrowKeys);
    var listener = new KeyboardListener(fakeInput, buffer);

    while (fakeInput.HasKeys)
    {
      listener.Listen();
    }

    Assert.Equal(1, buffer.CursorX);
    Assert.Equal(0, buffer.CursorY);
  }
}
