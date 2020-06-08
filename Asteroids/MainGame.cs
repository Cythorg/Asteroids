using Asteroids.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;

namespace Asteroids
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager Graphics;
        SpriteBatch spriteBatch;

        Camera camera;                      //methinks these should be capitalised ?
        Player player;
        List<Projectile> projectiles;

        SpriteFont spriteFont;

        List<SoundEffect> soundEffects; //need ot rework all sounds, however still proves useful to keep this here to remind me

        Random Random = new Random();




        public MainGame()
        {
            //this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 144d); // 144 fps
            this.Window.AllowUserResizing = true;
            //this.Window.ClientSizeChanged += OnWindowSizeChanged;
            this.Content.RootDirectory = "Content";

            Graphics = new GraphicsDeviceManager(this);                //need to do better here cmon dude
            Graphics.PreferredBackBufferHeight = 1080;                 //
            Graphics.PreferredBackBufferWidth = 1920;                  //


            //Graphics.IsFullScreen = true;

            soundEffects = new List<SoundEffect>(); //not really relevant, is it!?
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

            player = new Player();

            projectiles = new List<Projectile>();

            camera = new Camera(GraphicsDevice.Viewport);

            IsMouseVisible = true;
            
            Mouse.SetCursor(MouseCursor.FromTexture2D(Content.Load<Texture2D>("Assets/Textures/cursor1"), 8, 8));


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

            Player.Texture = this.Content.Load<Texture2D>("Assets/Textures/ship");

            Bullet.Texture = this.Content.Load<Texture2D>("Assets/Textures/bullet");

            Asteroid.Textures = new Dictionary<int, List<Texture2D>>
            {
                {0, new List<Texture2D>()
                    {
                        this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid8x1"),
                        this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid8x2"),
                        this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid8x3"),
                        this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid8x4")
                    } 
                },

                {1, new List<Texture2D>()
                    {
                        this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid16x1"),
                        this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid16x2"),
                        this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid16x3"),
                        this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid16x4")
                    } 
                },

                {2, new List<Texture2D>()
                    {
                        this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid32x1"),
                        this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid32x2"),
                        this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid32x3"),
                        this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid32x4")
                    } 
                }
            };

            //Asteroid.Textures = new List<Texture2D>();

            //Asteroid.Textures.Add(this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid8x1"));
            //Asteroid.Textures.Add(this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid8x2"));
            //Asteroid.Textures.Add(this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid8x3"));
            //Asteroid.Textures.Add(this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid8x4"));

            //Asteroid.Textures.Add(this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid16x1"));
            //Asteroid.Textures.Add(this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid16x2"));
            //Asteroid.Textures.Add(this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid16x3"));
            //Asteroid.Textures.Add(this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid16x4"));

            //Asteroid.Textures.Add(this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid32x1"));
            //Asteroid.Textures.Add(this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid32x2"));
            //Asteroid.Textures.Add(this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid32x3"));
            //Asteroid.Textures.Add(this.Content.Load<Texture2D>("Assets/Textures/Asteroid/asteroid32x4"));

            spriteFont = this.Content.Load<SpriteFont>("Assets/File");

            soundEffects.Add(Content.Load<SoundEffect>("Assets/Sounds/pew1"));
            soundEffects.Add(Content.Load<SoundEffect>("Assets/Sounds/pew2"));
            soundEffects.Add(Content.Load<SoundEffect>("Assets/Sounds/pew3"));
            soundEffects.Add(Content.Load<SoundEffect>("Assets/Sounds/pew4"));

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
        int cooldown = 0;               //should probably be moved to the player class
        protected override void Update(GameTime gameTime)
        {

            //Console.WriteLine(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X +" "+ GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y);


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (!(cooldown == 0)) //cooldown countdown, should be moved to player class methinks
            {
                cooldown -= 1;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed || Mouse.GetState().RightButton == ButtonState.Pressed) //rapid fire bby! possible power up in future, for now just fun to use ;)
            {                                                                                                                              //
                cooldown = 0;                                                                                                              //
            }                                                                                                                              //

            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed) //SHOOT! , Again could and probabyly should be moved to player class although projectiles list could prove problematic, maybe make static?
            {
                projectiles.Add(new Asteroid(Random.Next(3), player));
            }


            if (GamePad.GetState(PlayerIndex.One).Triggers.Right > 0 || Mouse.GetState().LeftButton == ButtonState.Pressed)     //AGAIN WITH THE MOVING STUFF TO THE PLAYER CLASS CMON TOM U SUCK MAN
            {
                if (cooldown == 0 && projectiles.Count < 10000)
                {
                    projectiles.Add(new Bullet(player, 6f));
                    cooldown = 10;
                }
                
            }

            player.Update(); //this stuff is clean though ;))
            camera.Update(player); //still not sure why gameTime is passed through although will keep this comment for sake of if I ever need gametime in the camera class as an argument lol

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black); //sets background to be black
            // TODO: Add your drawing code here
            spriteBatch.Begin(sortMode: SpriteSortMode.Immediate ,blendState: BlendState.Additive ,transformMatrix: camera.Transform); //transforms everything in the spritebatch by the matrix defined in the camera class, gives the illusion of movement!

            player.Draw(spriteBatch);

            //spriteBatch.DrawString(spriteFont: spriteFont, text: $"{projectiles.Count}", position: new Vector2(player.Position.X + 10, player.Position.Y +10), color: Color.White);

            //loops through all projectiles in a list and draws them; if the projectiles is "Dead" then the projectile is removed from the draw list         Projectiles currently includes: bullets, asteroids
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(spriteBatch);
                if (projectiles[i].Dead)
                { 
                    projectiles.RemoveAt(i--); 
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
