// DocumentHandlers.cs
namespace Sharpie;

public class DocumentHandler
{

  public async Task<Document> LoadDocument(string filePath)
  {
    if (string.IsNullOrWhiteSpace(filePath))
    {
      throw new DocumentHandlerException("The file path cannot be null");
    }

    try
    {
      string[] fileContent = await File.ReadAllLinesAsync(filePath);

      return new Document(filePath, fileContent);
    }
    catch (FileNotFoundException ex)
    {
      throw new DocumentHandlerException($"The document at {filePath} cannot be found.", ex);
    }
    catch (IOException ex)
    {
      throw new DocumentHandlerException("An error occured while reading the file.", ex);
    }
  }

  public async Task SaveDocument(Document document)
  {
    if (string.IsNullOrWhiteSpace(document.FilePath))
    {
      throw new DocumentHandlerException("The file path cannot be null.");
    }

    try
    {
      await File.WriteAllLinesAsync(document.FilePath, document.Body);
    }
    catch (DirectoryNotFoundException ex)
    {
      throw new DocumentHandlerException($"Directory could not be found. File Path: {document.FilePath}.", ex);
    }
    catch (IOException ex)
    {
      throw new DocumentHandlerException("An error occured while saving the file.", ex);
    }
  }
}

public class DocumentHandlerException : Exception
{
  public DocumentHandlerException(string message)
    : base(message) { }

  public DocumentHandlerException(string message, Exception innerException)
    : base(message, innerException) { }
}
