using Xunit;

namespace Sharpie.Tests;

public class TextBufferTests
{

  public class InsertEnterMethod
  {
    // Initialize Buffer for use across tests
    private readonly TextBuffer _buffer = new TextBuffer();

    [Fact]
    public void Creates_NewLineAbove_WhenCursorIsAt_BeginningOfLine()
    {
      string secondLine = "This will become the second line.";
      _buffer.Lines = [secondLine];

      _buffer.InsertEnter();

      Assert.Equal("", _buffer.Lines[0]);
      Assert.Equal(secondLine, _buffer.Lines[1]);
    }

    [Fact]
    public void Creates_NewLine_Above_WhenCursorIsAt_EndOfLine()
    {
      _buffer.Lines = ["First Line"];
      _buffer.CursorX = 10;

      _buffer.InsertEnter();

      Assert.Equal(2, _buffer.Lines.Count);
      Assert.Equal("First Line", _buffer.Lines[0]);
      Assert.Equal("", _buffer.Lines[1]);
    }

    [Fact]
    public void InsertEnter_SplitsLine_AtCursorPosition_AndMovesCursorPositionToStartOfNewLine()
    {
      _buffer.Lines = ["Hello Clint"];
      _buffer.InsertEnter();

      Assert.Equal(2, _buffer.Lines.Count);
      Assert.Equal(0, _buffer.CursorX);
      Assert.Equal(1, _buffer.CursorY);
    }

    [Fact]
    public void InsertEnter_InsertsLine_BetweenExistingLines()
    {
      string displacedLine = "This line will become the 3rd line.";
      _buffer.Lines = ["Hello Clint"];
      _buffer.Lines.Add(displacedLine);
      _buffer.CursorX = 5;

      _buffer.InsertEnter();

      Assert.Equal(3, _buffer.Lines.Count);
      Assert.Equal(displacedLine, _buffer.Lines[2]);
    }
  }

  public class InsertAddCharMethod
  {
    private readonly TextBuffer _buffer = new TextBuffer();

    [Fact]
    public void InsertsACharacter_AtPositionOfCursor()
    {
      _buffer.Lines = ["Insertbetween"];
      _buffer.CursorX = 6;

      _buffer.InsertChar(" ");

      Assert.Equal("Insert between", _buffer.Lines[0]);
    }
  }

  public class BackspaceMethod
  {
    private readonly TextBuffer _buffer = new TextBuffer();

    [Fact]
    public void RemovesCharacter_FromBehindCursorPosition()
    {
      _buffer.Lines = ["No Space"];
      _buffer.CursorX = 3;

      _buffer.Backspace();

      Assert.Equal("NoSpace", _buffer.Lines[0]);
    }

    [Fact]
    public void MovesCursor_BackOnePosition_UnlessAtFirstPosition()
    {
      _buffer.Lines = ["xxx"];
      _buffer.CursorX = 1;

      _buffer.Backspace();
      _buffer.Backspace();
      _buffer.Backspace();
      _buffer.Backspace();

      Assert.Equal(2, _buffer.Lines[0].Length);
      Assert.Equal(0, _buffer.CursorX);
    }
  }

  public class MoveCursorLeftMethod
  {
    private readonly TextBuffer _buffer = new TextBuffer();

    [Fact]
    public void MovesCursor_Left_OnePosition()
    {
      _buffer.Lines = ["xxx"];
      _buffer.CursorX = 1;

      _buffer.MoveCursorLeft();

      Assert.Equal(0, _buffer.CursorX);
    }

    [Fact]
    public void CursorPositionCannotBe_LessThanZero()
    {
      _buffer.CursorX = 0;

      _buffer.MoveCursorLeft();
      _buffer.MoveCursorLeft();

      Assert.Equal(0, _buffer.CursorX);
    }
  }

  public class MoveCursorRightMethod
  {
    private readonly TextBuffer _buffer = new TextBuffer();

    [Fact]
    public void MovesCursor_Right_OnePosition()
    {
      _buffer.Lines = ["xxx"];
      _buffer.CursorX = 0;

      _buffer.MoveCursorRight();

      Assert.Equal(1, _buffer.CursorX);
    }

    [Fact]
    public void CursorPositionCannotBe_GreaterThanLineIndex()
    {
      _buffer.Lines = ["xxx"];
      _buffer.CursorX = 2;

      _buffer.MoveCursorRight();

      Assert.Equal(2, _buffer.CursorX);
    }
  }

  public class MoveCursorDownMethod
  {
    private readonly TextBuffer _buffer = new TextBuffer();

    [Fact]
    public void MoveCursor_Down_OnePosition()
    {
      _buffer.Lines.Insert(1, "second line");
      _buffer.Lines.Insert(2, "third line");

      _buffer.MoveCursorDown();
      _buffer.MoveCursorDown();

      Assert.Equal(2, _buffer.CursorY);
    }

    [Fact]
    public void CannotMoveCursor_BeyondExisitingLineIndex()
    {
      _buffer.MoveCursorDown();

      Assert.Equal(0, _buffer.CursorY);
    }
  }

  public class MoveCursorUpMethod
  {
    private readonly TextBuffer _buffer = new TextBuffer();

    [Fact]
    public void MovesCursor_Up_OnePosition()
    {
      _buffer.Lines.Insert(1, "second line");
      _buffer.CursorY = 1;

      _buffer.MoveCursorUp();

      Assert.Equal(0, _buffer.CursorY);
    }

    [Fact]
    public void CannotMoveCursor_AboveFirstLine()
    {
      _buffer.Lines.Insert(1, "second line");
      _buffer.CursorY = 1;

      _buffer.MoveCursorUp();
      _buffer.MoveCursorUp();
      _buffer.MoveCursorUp();

      Assert.Equal(0, _buffer.CursorY);
    }
  }
}
