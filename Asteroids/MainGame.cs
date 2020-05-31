using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Asteroids
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager Graphics;
        SpriteBatch spriteBatch;


        Player player;
        List<Projectile> projectiles;

        List<SoundEffect> soundEffects;


        public MainGame()
        {
            //this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 144d); // 144 fps
            this.Window.AllowUserResizing = true;
            //this.Window.ClientSizeChanged += OnWindowSizeChanged;
            this.Content.RootDirectory = "Content";

            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferHeight = 1080;
            Graphics.PreferredBackBufferWidth = 1920;

            soundEffects = new List<SoundEffect>();
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

            player = new Player
            {
                Viewport = GraphicsDevice.Viewport,
                Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2)
            };
            //player.Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            //projectile = new Projectile(new float(), new Vector2(), new Vector2(), this.Content.Load<Texture2D>("LGBT"));
            //projectile = new Projectile(new Vector2(50,50), new Vector2(50,50), this.Content.Load<Texture2D>("bullet"));
            projectiles = new List<Projectile>();


            IsMouseVisible = true;
            
            Mouse.SetCursor(MouseCursor.FromTexture2D(Content.Load<Texture2D>("cursor1"), 8, 8));


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

            Player.Texture = this.Content.Load<Texture2D>("ship");
            Player.RedShip = this.Content.Load<Texture2D>("redship");
            Player.GreenShip = this.Content.Load<Texture2D>("greenship");
            Player.BlueShip = this.Content.Load<Texture2D>("blueship");

            Bullet.Texture = this.Content.Load<Texture2D>("bullet");

            soundEffects.Add(Content.Load<SoundEffect>("pew1"));
            soundEffects.Add(Content.Load<SoundEffect>("pew2"));
            soundEffects.Add(Content.Load<SoundEffect>("pew3"));
            soundEffects.Add(Content.Load<SoundEffect>("pew4"));




            //projectile.Texture = this.Content.Load<Texture2D>("bullet");
            //projectile.projectile = this.Content.Load<Texture2D>("projectile");
            //ship = this.Content.Load<Texture2D>("LGBT");
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
        /// 

        Random random = new Random();
        int cooldown = 0;
        protected override void Update(GameTime gameTime)
        {

            //Console.WriteLine(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X +" "+ GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y);


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Right))
            //    position = new Vector2(position.X + 6, position.Y);
            if (!(cooldown == 0))
            {
                cooldown -= 1;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            {
                cooldown = 0;
            }

            if (GamePad.GetState(PlayerIndex.One).Triggers.Right > 0)
            {
                if (cooldown == 0 && projectiles.Count < 100)
                {
                    projectiles.Add(new Bullet(player, 6f));
                    //projectiles.Add(new Projectile(player, this.Content.Load<Texture2D>("bullet")));
                    soundEffects[random.Next(4)].CreateInstance().Play();
                    cooldown = 10;
                }
                
            }

            player.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(Content.Load<Texture2D>("cursor2"), position: new Vector2(Mouse.GetState().X, Mouse.GetState().Y), origin: new Vector2(8, 8));

            player.Draw(spriteBatch);

            //loops through all projectiles in a list and draws them; if the position of the projectile is not within the Viewport bounds the projectile is removed from the draw list

            List<int> projectilesCheck = new List<int>();
            projectilesCheck.Clear();

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(spriteBatch);
                if (projectiles[i].Position.X > GraphicsDevice.Viewport.Width || projectiles[i].Position.Y > GraphicsDevice.Viewport.Height || projectiles[i].Position.X < 0 || projectiles[i].Position.Y < 0)
                {
                    projectiles.RemoveAt(i);
                    i--;

                }
            }
            //if (player.Velocity.Length() > 10.01)
            //{
            //    projectiles.Add(new Projectile(player, Content.Load<Texture2D>("LGBT")));
            //    spriteBatch.Draw(Content.Load<Texture2D>("LGBT"), position: player.Position, rotation: player.Rotation, origin: new Vector2(8, 8));
            //}

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
