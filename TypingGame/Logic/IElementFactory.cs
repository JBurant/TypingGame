using System.Collections.Generic;

namespace TypingGame.Logic
{
    public interface IElementFactory
    {
        List<Element> GetElements(int noOfElements);
    }
}