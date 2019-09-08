using System;
using System.Collections.Generic;

namespace TypingGame.Logic
{
    public class ElementFactory : IElementFactory
    {
        private readonly IColorRGBAFactory colorRGBAFactory;
        private readonly IStringFileReader stringFileReader;
        private readonly List<string> stringsToAdd;
        private readonly Random random;

        public ElementFactory(IStringFileReader stringFileReader, IColorRGBAFactory colorRGBAFactory)
        {
            this.stringFileReader = stringFileReader;
            this.colorRGBAFactory = colorRGBAFactory;
            stringsToAdd = new List<string>();
            random = new Random();
        }

        public List<Element> GetElements(int noOfElements)
        {
            if(stringsToAdd.Count < noOfElements)
            {
                stringsToAdd.AddRange(stringFileReader.GetBatchOfStrings(noOfElements));
            }

            var elements = new List<Element>();

            for (int i = 0; i < noOfElements; i++)
            {
                var coordinateY = (decimal)random.NextDouble() * (WindowBoundaries.MAX_HEIGHT - WindowBoundaries.MIN_HEIGHT) + WindowBoundaries.MIN_HEIGHT;
                var element = new Element(stringsToAdd[0], colorRGBAFactory.GetColor(), GameConfiguration.BASE_SPEED, 0, coordinateY);
                elements.Add(element);
                stringsToAdd.RemoveAt(0);
            }

            return elements;
        }
    }
}
