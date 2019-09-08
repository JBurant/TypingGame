using System;
using System.Collections.Generic;
using System.Linq;

namespace TypingGame.Logic
{
    public class WorldManager : IWorldManager
    {
        private readonly IElementFactory ElementFactory;
        private int removedElementsCounter;

        private decimal totalTimer;
        private decimal lastTimeElementAdded;

        public List<Element> Elements { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }
        public int Lives { get; set; }

        public WorldManager(IElementFactory elementFactory)
        {
            ElementFactory = elementFactory;
            Elements = new List<Element>();
            Lives = GameConfiguration.INITIAL_LIVES;
        }

        public void Move(decimal deltaTime)
        {
            foreach(var element in Elements)
            {
                element.Move(deltaTime);
            }
        }

        public void HandleInput(List<string> stringKeys)
        {
            foreach (var stringKey in stringKeys)
            {
                var hitResult = TryRemoveLetter(stringKey);

                if (hitResult)
                {
                    Score += (1 + Level / 2) * 10;
                }
            }
        }

        public GameState CheckEndConditions()
        {
            if (Lives < 0)
            {
                return GameState.FAILED;
            }

            if (Level > GameConfiguration.MAX_LEVEL)
            {
                return GameState.WON;
            }

            return GameState.RUNNING;
        }

        public void UpdateWorldState(decimal deltaTime)
        {
            totalTimer += deltaTime;

            foreach (var element in Elements)
            {
                if (element.X >= WindowBoundaries.MAX_WIDTH)
                {
                    Lives--;
                }
            }

            if (removedElementsCounter > (Level + 1) * GameConfiguration.ELEMENTS_PER_LEVEL)
            {
                Level++;
            }
        }

        public void AddNewElements(int noOfElements)
        {
            Elements.AddRange(ElementFactory.GetElements(noOfElements));
        }

        public int GetNumberOfElementsToAdd()
        {
            var noOfElements = (int)Math.Floor(((totalTimer - lastTimeElementAdded) / 5) * (Level / 2 + 1));
            lastTimeElementAdded = noOfElements > 0 ? totalTimer : lastTimeElementAdded;
            return noOfElements;
        }

        public void RemoveOldElements()
        {
            removedElementsCounter += Elements.RemoveAll(x => string.IsNullOrWhiteSpace(x.Text));
            removedElementsCounter += Elements.RemoveAll(x => x.X > WindowBoundaries.MAX_WIDTH);
        }

        private bool TryRemoveLetter(string letterToRemove)
        {
            var hitElement = Elements.FirstOrDefault(x => x.IsHit);

            if (hitElement != null)
            {
                return Elements.FirstOrDefault(x => x.IsHit).TryHit(letterToRemove);
            }
            else
            {
                var hitResult = false;
                var index = 0;
                while(!hitResult && index < Elements.Count)
                {
                    hitResult = Elements[index].TryHit(letterToRemove);
                    index++;
                }

                return hitResult;
            }
        }
    }
}
