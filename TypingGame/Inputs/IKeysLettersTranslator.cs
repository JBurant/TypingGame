using Microsoft.Xna.Framework.Input;

namespace TypingGame.Inputs
{
    public interface IKeysLettersTranslator
    {
        string TranslateKeyToString(Keys inputKey);
    }
}