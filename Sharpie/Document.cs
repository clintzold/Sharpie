// Document.cs
namespace Sharpie;

public class Document(string filePath, string[] fileContent)
{
  public string[] Body { get; set; } = fileContent;
  public string FilePath { get; set; } = filePath;
}
