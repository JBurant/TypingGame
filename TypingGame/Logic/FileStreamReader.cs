using System.IO;

namespace TypingGame.Logic
{
    public class FileStreamReader : IFileStreamReader
    {
        private StreamReader streamReader;

        public bool EndOfStream { get; }

        public FileStreamReader()
        {
            streamReader = new StreamReader(SourceFileConfiguration.FILENAME);
        }

        public string ReadLine()
        {
            return streamReader.ReadLine();
        }        
    }
}
