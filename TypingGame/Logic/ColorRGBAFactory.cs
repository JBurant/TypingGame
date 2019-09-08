using System;

namespace TypingGame.Logic
{
    public class ColorRGBAFactory : IColorRGBAFactory
    {
        private readonly Random random;

        public ColorRGBAFactory()
        {
            random = new Random();
        }

        public ColorRGBA GetColor()
        {
            var index = random.Next(3);

            switch (index)
            {
                case 0: return new ColorRGBA { R = 256, G = 0, B = 0, Alpha = 0 };
                case 1: return new ColorRGBA { R = 0, G = 0, B = 256, Alpha = 0 };
                case 2: return new ColorRGBA { R = 0, G = 256, B = 0, Alpha = 0 };
                default: return new ColorRGBA { R = 256, G = 256, B = 256, Alpha = 0 };
            }
        }
    }
}
