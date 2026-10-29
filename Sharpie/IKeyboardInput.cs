// IKeyboardInput.cs
namespace Sharpie;

public interface IKeyboardInput
{
  ConsoleKeyInfo ReadKey();
}

public class KeyboardInput : IKeyboardInput
{
  public ConsoleKeyInfo ReadKey() => Console.ReadKey(true);
}
