using Ninject.Modules;
using TypingGame.Logic;
using TypingGame.Inputs;

namespace TypingGameTypingGame.Ninject
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IWorldManager>().To<WorldManager>();
            Bind<IElementFactory>().To<ElementFactory>();
            Bind<IColorRGBAFactory>().To<ColorRGBAFactory>();

            Bind<IKeyboardLetterToStringMapper>().To<KeyboardLetterToStringMapper>();
            Bind<IStringFileReader>().To<StringFileReader>();
            Bind<IKeysLettersTranslator>().To<KeysLettersTranslator>();
            Bind<IFileStreamReader>().To<FileStreamReader>();
        }
    }
}
