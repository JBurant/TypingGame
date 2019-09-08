using Ninject;
using System;
using System.Reflection;
using TypingGame.Inputs;
using TypingGame.Logic;

namespace TypingGame
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var worldManager = kernel.Get<IWorldManager>();
            var keyboardLetterToStringMapper = kernel.Get<IKeyboardLetterToStringMapper>();

            using (var game = new Glue.TypingGame(worldManager, keyboardLetterToStringMapper))
                game.Run();
        }
    }
}
