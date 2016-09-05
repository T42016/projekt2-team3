using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MineSweeperLogic;

namespace MineSweeperMonoGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MineSweeperMonoGame : Game, IServiceBus
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private KeyboardState prevState;
        private MineSweeperGame gameLogic;
        private Random rnd;
        private Texture2D tiles, background;
        private SpriteFont spriteFont;
        const int TileSize = 64;

        public MineSweeperMonoGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            rnd = new Random();
            gameLogic = new MineSweeperGame(10, 10, 10, this);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tiles = Content.Load<Texture2D>("tiles");
            background = Content.Load<Texture2D>("background");
            spriteFont = Content.Load<SpriteFont>("spriteFont");
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState kbState = Keyboard.GetState();

            if (gameLogic.State == GameState.Playing)
            {
                if (kbState.IsKeyDown(Keys.Left) && prevState.IsKeyUp(Keys.Left))
                    gameLogic.MoveCursorLeft();
                if (kbState.IsKeyDown(Keys.Right) && prevState.IsKeyUp(Keys.Right))
                    gameLogic.MoveCursorRight();
                if (kbState.IsKeyDown(Keys.Up) && prevState.IsKeyUp(Keys.Up))
                    gameLogic.MoveCursorUp();
                if (kbState.IsKeyDown(Keys.Down) && prevState.IsKeyUp(Keys.Down))
                    gameLogic.MoveCursorDown();
                if (kbState.IsKeyDown(Keys.Space) && prevState.IsKeyUp(Keys.Space))
                    gameLogic.ClickCoordinate();
                if (kbState.IsKeyDown(Keys.Enter) && prevState.IsKeyUp(Keys.Enter))
                    gameLogic.FlagCoordinate();
            }
            else
            {
                if (kbState.IsKeyDown(Keys.Space) && prevState.IsKeyUp(Keys.Space))
                    gameLogic.ResetBoard();
            }

            prevState = kbState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            drawX = 30;
            drawY = 30;
            gameLogic.DrawBoard();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private int drawX = 30;
        private int drawY = 30;

        public void Write(string text)
        {
            switch (text)
            {
                case ". ":
                    spriteBatch.Draw(tiles, new Vector2(drawX, drawY), new Rectangle(0, TileSize * 2, TileSize, TileSize), Color.Gray);
                    break;
                case "! ":
                    spriteBatch.Draw(tiles, new Vector2(drawX, drawY), new Rectangle(0, 0, TileSize, TileSize), Color.White);
                    spriteBatch.Draw(tiles, new Vector2(drawX, drawY), new Rectangle(TileSize * 3, 0, TileSize, TileSize), Color.White);
                    break;
                case "X ":
                    spriteBatch.Draw(tiles, new Vector2(drawX, drawY), new Rectangle(TileSize * 2, 0, TileSize, TileSize), Color.White);
                    break;
                case "? ":
                    spriteBatch.Draw(tiles, new Vector2(drawX, drawY), new Rectangle(0, 0, TileSize, TileSize), Color.White);
                    break;
                case "1 ":
                    spriteBatch.Draw(tiles, new Vector2(drawX, drawY), new Rectangle(0, TileSize * 2, TileSize, TileSize), Color.Gray);
                    spriteBatch.DrawString(spriteFont, "1", new Vector2(drawX + 15, drawY + 5), Color.Blue);
                    break;
                case "2 ":
                    spriteBatch.Draw(tiles, new Vector2(drawX, drawY), new Rectangle(0, TileSize * 2, TileSize, TileSize), Color.Gray);
                    spriteBatch.DrawString(spriteFont, "2", new Vector2(drawX + 15, drawY + 5), Color.Green);
                    break;
                case "3 ":
                    spriteBatch.Draw(tiles, new Vector2(drawX, drawY), new Rectangle(0, TileSize * 2, TileSize, TileSize), Color.Gray);
                    spriteBatch.DrawString(spriteFont, "3", new Vector2(drawX + 15, drawY + 5), Color.Red);
                    break;
                case "4 ":
                    spriteBatch.Draw(tiles, new Vector2(drawX, drawY), new Rectangle(0, TileSize * 2, TileSize, TileSize), Color.Gray);
                    spriteBatch.DrawString(spriteFont, "4", new Vector2(drawX + 15, drawY + 5), Color.Magenta);
                    break;
                case "5 ":
                    spriteBatch.Draw(tiles, new Vector2(drawX, drawY), new Rectangle(0, TileSize * 2, TileSize, TileSize), Color.Gray);
                    spriteBatch.DrawString(spriteFont, "5", new Vector2(drawX + 15, drawY + 5), Color.Cyan);
                    break;
                case "6 ":
                    spriteBatch.Draw(tiles, new Vector2(drawX, drawY), new Rectangle(0, TileSize * 2, TileSize, TileSize), Color.Gray);
                    spriteBatch.DrawString(spriteFont, "6", new Vector2(drawX + 15, drawY + 5), Color.Yellow);
                    break;
                case "7 ":
                    spriteBatch.Draw(tiles, new Vector2(drawX, drawY), new Rectangle(0, TileSize * 2, TileSize, TileSize), Color.Gray);
                    spriteBatch.DrawString(spriteFont, "7", new Vector2(drawX + 15, drawY + 5), Color.DarkGreen);
                    break;
                case "8 ":
                    spriteBatch.Draw(tiles, new Vector2(drawX, drawY), new Rectangle(0, TileSize * 2, TileSize, TileSize), Color.Gray);
                    spriteBatch.DrawString(spriteFont, "8", new Vector2(drawX + 15, drawY + 5), Color.DarkBlue);
                    break; 
            }
            drawX += TileSize;
        }

        public void Write(string text, ConsoleColor backgroundColor)
        {
            Write(text);
            spriteBatch.Draw(tiles, new Vector2(drawX - TileSize, drawY), new Rectangle(0, TileSize, TileSize, TileSize), new Color(160, 160, 160, 160));
        }

        public void WriteLine()
        {
            drawY += TileSize;
            drawX = 30;
        }

        public int Next(int maxValue)
        {
            return rnd.Next(maxValue);
        }
    }
}
