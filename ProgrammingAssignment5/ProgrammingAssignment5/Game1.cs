using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TeddyMineExplosion;

namespace ProgrammingAssignment5
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int WINDOW_WIDTH = 800;
        const int WINDOW_HEIGHT = 600;

        Texture2D explosionSprite;
        Texture2D mineSprite;
        Texture2D teddybearSprite;

        List<Mine> mines = new List<Mine>();
        List<Explosion> explosions = new List<Explosion>();
        List<TeddyBear> teddyBears = new List<TeddyBear>();

        int elapsedTeddySpawn;

        // click processing
        bool leftClickStarted = false;
        bool leftButtonReleased = true;

        Random rand = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            explosionSprite = Content.Load<Texture2D>("explosion");
            mineSprite = Content.Load<Texture2D>("mine");
            teddybearSprite = Content.Load<Texture2D>("teddybear");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Spawn new Teddy
            #region
            elapsedTeddySpawn += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedTeddySpawn >= rand.Next(1000, 3001))
            {
                elapsedTeddySpawn = 0;

                var velocity = new Vector2(
                    (float)(rand.NextDouble() - 0.5),
                    (float)(rand.NextDouble() - 0.5));

                teddyBears.Add(new TeddyBear(teddybearSprite, velocity, WINDOW_WIDTH, WINDOW_HEIGHT));
            }
            #endregion

            //Check for create new mine
            #region
            MouseState mouse = Mouse.GetState();

            // check for right click started

            if (mouse.LeftButton == ButtonState.Pressed &&
                leftButtonReleased)
            {
                leftClickStarted = true;
                leftButtonReleased = false;
            }
            else if (mouse.LeftButton == ButtonState.Released)
            {
                leftButtonReleased = true;

                // if right click finished, add new pickup to list
                if (leftClickStarted)
                {
                    leftClickStarted = false;


                    mines.Add(new Mine(mineSprite, mouse.X, mouse.Y));
                }
            }
            #endregion

            //Update Teddies
            #region
            foreach (var teddyBear in teddyBears)
            {
                teddyBear.Update(gameTime);
            }
            #endregion

            //Update Explosions
            #region
            foreach (var explosion in explosions)
            {
                explosion.Update(gameTime);
            }

            #endregion

            //Check collisions
            #region
            foreach (var teddyBear in teddyBears)
            {
                foreach (var mine in mines)
                {
                    if (teddyBear.CollisionRectangle.Intersects(mine.CollisionRectangle))
                    {
                        var coords = mine.CollisionRectangle;
                        explosions.Add(new Explosion(
                            explosionSprite, 
                            coords.X + mine.CollisionRectangle.Width / 2,
                            coords.Y + mine.CollisionRectangle.Height / 2));

                        teddyBear.Active = false;
                        mine.Active = false;
                    }
                }
            }
            #endregion

            //Remove inactive
            #region
            teddyBears.Where(t => !t.Active).ToList().ForEach(t => teddyBears.Remove(t));
            mines.Where(m => !m.Active).ToList().ForEach(m => mines.Remove(m));
            explosions.Where(e => !e.Playing).ToList().ForEach(e => explosions.Remove(e));
            #endregion

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

            // TODO: Add your drawing code here
            foreach (var mine in mines)
            {
                mine.Draw(spriteBatch);
            }

            foreach (var explosion in explosions)
            {
                explosion.Draw(spriteBatch);
            }

            foreach (var teddyBear in teddyBears)
            {
                teddyBear.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
