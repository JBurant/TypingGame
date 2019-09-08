namespace TypingGame.Logic
{
    public interface IFileStreamReader
    {
        string ReadLine();

        bool EndOfStream { get; }
    }
}
