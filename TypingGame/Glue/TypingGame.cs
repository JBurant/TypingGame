using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TypingGame.Inputs;
using TypingGame.Logic;

namespace TypingGame.Glue
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TypingGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        private readonly IWorldManager WorldManager;
        private readonly IKeyboardLetterToStringMapper KeyboardLetterToStringMapper;
        private GameState GameState;

        public TypingGame(IWorldManager worldManager, IKeyboardLetterToStringMapper keyboardLetterToStringMapper)
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            WorldManager = worldManager;
            graphics.IsFullScreen = true;
            KeyboardLetterToStringMapper = keyboardLetterToStringMapper;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            GameState = GameState.RUNNING;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("GameFont");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var deltaTime = (decimal)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(GameState == GameState.RUNNING)
            {
                WorldManager.HandleInput(KeyboardLetterToStringMapper.MapKeysToStrings(Keyboard.GetState().GetPressedKeys()));
                WorldManager.Move(deltaTime);
                WorldManager.UpdateWorldState(deltaTime);
                GameState = WorldManager.CheckEndConditions();

                if (GameState == GameState.RUNNING)
                {
                    WorldManager.AddNewElements(WorldManager.GetNumberOfElementsToAdd());
                    WorldManager.RemoveOldElements();
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            var width = Window.ClientBounds.Width;
            var height = Window.ClientBounds.Height;
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, $"Level: {WorldManager.Level}", new Vector2(WindowBoundaries.LEVEL_COUNTER_X * width, WindowBoundaries.LEVEL_COUNTER_Y * height), Color.White);
            spriteBatch.DrawString(spriteFont, $"Score: {WorldManager.Score}", new Vector2(WindowBoundaries.SCORE_COUNTER_X * width, WindowBoundaries.SCORE_COUNTER_Y * height), Color.White);
            spriteBatch.DrawString(spriteFont, $"Lives: {WorldManager.Lives}", new Vector2(WindowBoundaries.LIVES_COUNTER_X * width, WindowBoundaries.LIVES_COUNTER_Y * height), Color.White);

            switch (GameState)
            {
                case GameState.RUNNING:
                    {
                        DrawGame(width, height);
                        break;
                    }
                case GameState.WON:
                    {
                        DrawWon(width, height);
                        break;
                    }
                case GameState.FAILED:
                    {
                        DrawFailed(width, height);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void DrawGame(int width, int height)
        {
            var strings = WorldManager.Elements;

            for (int i = 0; i < strings.Count; i++)
            {
                spriteBatch.DrawString(
                    spriteFont,
                    strings[i].Text,
                    new Vector2((float)strings[i].X * width, (float)strings[i].Y * height),
                    new Color(strings[i].Color.R, strings[i].Color.G, strings[i].Color.B, strings[i].Color.Alpha));
            }
        }

        private void DrawWon(int width, int height)
        {
            spriteBatch.DrawString(spriteFont, "YOU WON!", new Vector2(0.4f * width, 0.4f * height), Color.White);
        }

        private void DrawFailed(int width, int height)
        {
            spriteBatch.DrawString(spriteFont, "YOU FAILED!", new Vector2(0.4f * width, 0.4f * height), Color.White);
        }
    }
}
