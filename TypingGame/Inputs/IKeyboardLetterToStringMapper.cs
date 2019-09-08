using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TypingGame.Inputs
{
    public interface IKeyboardLetterToStringMapper
    {
        List<string> MapKeysToStrings(Keys[] inputKeys);
    }
}