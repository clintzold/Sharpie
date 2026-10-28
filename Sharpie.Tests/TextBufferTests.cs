using Xunit;
using Sharpie;

namespace Sharpie.Tests;

public class TextBufferTests
{

  public class InsertEnterMethod
  {
    // Initialize Buffer for use across tests
    private readonly TextBuffer _buffer = new TextBuffer();

    [Fact]
    public void Initializes_WithProperFieldValues()
    {

      Assert.Equal(0, _buffer.CursorY);
      Assert.Equal(0, _buffer.CursorX);
      Assert.Equal("", _buffer.Lines[0]);
    }

    [Fact]
    public void LinesSetter_AddsStringsAsLines()
    {
      string firstLine = "This is the first line.";
      string secondLine = "This is the second line.";

      _buffer.Lines = [firstLine];
      _buffer.Lines.Add(secondLine);

      Assert.Equal(firstLine, _buffer.Lines[0]);
      Assert.Equal(secondLine, _buffer.Lines[1]);
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
    public void RemovesCharacter_FromPositionBehindCursor()
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

      Assert.Equal(2, _buffer.Lines[0].Length);
      Assert.Equal(0, _buffer.CursorX);
    }
  }
}
