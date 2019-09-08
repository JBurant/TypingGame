using System.Collections.Generic;

namespace TypingGame.Logic
{
    public interface IStringFileReader
    {
        List<string> GetBatchOfStrings(int requestedNoOfElements);
    }
}
