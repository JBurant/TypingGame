using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace TypingGame.Inputs
{
    public class KeyboardLetterToStringMapper : IKeyboardLetterToStringMapper
    {
        private List<Keys> oldKeys;
        private IKeysLettersTranslator keysLettersTranslator;

        public KeyboardLetterToStringMapper(IKeysLettersTranslator keysLettersTranslator)
        {
            oldKeys = new List<Keys>();
            this.keysLettersTranslator = keysLettersTranslator;
        }

        public List<string> MapKeysToStrings(Keys [] inputKeys)
        {
            var stringsForKeys = new List<string>();
            var freshlyPressedKeys = inputKeys.Where(x => !oldKeys.Contains(x)).ToArray();

            foreach (var key in freshlyPressedKeys)
            {
                stringsForKeys.Add(keysLettersTranslator.TranslateKeyToString(key));
            }

            oldKeys = inputKeys.ToList();

            return stringsForKeys;
        }
    }
}
