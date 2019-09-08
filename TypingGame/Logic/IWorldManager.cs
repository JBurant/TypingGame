using System.Collections.Generic;

namespace TypingGame.Logic
{
    public interface IWorldManager
    {
        List<Element> Elements { get; }
        int Level { get; set; }
        int Score { get; set; }
        int Lives { get; set; }

        void UpdateWorldState(decimal deltaTime);

        void Move(decimal deltaTime);

        void HandleInput(List<string> stringKeys);

        GameState CheckEndConditions();

        void AddNewElements(int noOfElements);

        void RemoveOldElements();

        int GetNumberOfElementsToAdd();
    }
}
