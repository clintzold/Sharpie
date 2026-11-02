// DocumentHandlerTests.cs
using Xunit;

namespace Sharpie.Tests;

public class DocumentHandlerTests
{
  private readonly DocumentHandler _handler = new();

  [Fact]
  public async Task LoadDocument_ThrowsErrorWhenFilePath_IsNullOrWhiteSpace()
  {
    var exception = await Record.ExceptionAsync(async () => await _handler.LoadDocument(" "));

    Assert.IsType<DocumentHandlerException>(exception);
  }

  [Fact]
  public async Task LoadDocument_ThrowsErrorWhenFilePath_IsNotFound()
  {
    var exception = await Record.ExceptionAsync(async () => await _handler.LoadDocument("nonexistent"));

    Assert.IsType<DocumentHandlerException>(exception);
  }

  [Fact]
  public async Task SaveDocument_ThrowsErrorWhenFilePath_IsNullOrWhiteSpace()
  {
    Document document = new("", ["This won't be saved"]);
    var exception = await Record.ExceptionAsync(async () => await _handler.SaveDocument(document));

    Assert.IsType<DocumentHandlerException>(exception);
  }

  [Fact]
  public async Task SaveDocument_ThrowsErrorWhen_DirectoryIsNotFound()
  {
    Document document = new("fakedir/fakefile.txt", ["This won't be saved"]);
    var exception = await Record.ExceptionAsync(async () => await _handler.SaveDocument(document));

    Assert.IsType<DocumentHandlerException>(exception);
  }

  [Fact]
  public async Task SaveDocument_WritesFileToCurrentDir()
  {
    Document document = new("save-me.txt", ["This will be saved"]);

    await _handler.SaveDocument(document);

    string[] file = await File.ReadAllLinesAsync(document.FilePath);

    Assert.Equal("This will be saved", file[0]);

    File.Delete(document.FilePath);
  }
}
