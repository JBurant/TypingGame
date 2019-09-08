using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TypingGame.Inputs
{
    public class KeysLettersTranslator : IKeysLettersTranslator
    {
        private static Dictionary<Keys, string> dictionary = new Dictionary<Keys, string>
        {
        {Keys.A, "a"},
        {Keys.B, "b"},
        {Keys.C, "c"},
        {Keys.D, "d"},
        {Keys.E, "e"},
        {Keys.F, "f"},
        {Keys.G, "g"},
        {Keys.H, "h"},
        {Keys.I, "i"},
        {Keys.J, "j"},
        {Keys.K, "k"},
        {Keys.L, "l"},
        {Keys.M, "m"},
        {Keys.N, "n"},
        {Keys.O, "o"},
        {Keys.P, "p"},
        {Keys.Q, "q"},
        {Keys.R, "r"},
        {Keys.S, "s"},
        {Keys.T, "t"},
        {Keys.U, "u"},
        {Keys.V, "v"},
        {Keys.W, "w"},
        {Keys.X, "x"},
        {Keys.Y, "y"},
        {Keys.Z, "z"},
        };

        public string TranslateKeyToString(Keys inputKey)
        {
            if(dictionary.TryGetValue(inputKey, out string retVal))
            {
                return retVal;
            }

            return null;
        }
    }
}
