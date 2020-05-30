using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Player
    {
        public static Texture2D Texture;
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public float Rotation { get; set; }
        public Viewport Viewport { get; set; }

        int MaxSpeed = 10;


        public Player()
        {
        }

        public void Update()
        {
            Acceleration = new Vector2(0, 0);

            if (Position.X > Viewport.Width || Position.Y > Viewport.Height || Position.X < 0 || Position.Y < 0)
            {
                Position = new Vector2(Viewport.Width/2, Viewport.Height / 2);
                Velocity = new Vector2(0,0);
                Rotation = 0;
            }



            if (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed)
            {
                Position = new Vector2(Viewport.Width / 2, Viewport.Height / 2);
                Position = new Vector2(0, Viewport.Height / 2);
                Velocity = new Vector2(0, 0);
            }
            //if (Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y) > Settings.Deadzone || Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) > Settings.Deadzone)
            //need to normalize the deadzone setting; it is currently a square around the thumbstick and should be a circle


            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Length() > Settings.Deadzone)
            {
                Acceleration = new Vector2(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, -GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y)*0.5f;
                //Velocity = Vector2.Add(Velocity, Acceleration);
                //Position = new Vector2(Position.X + (Settings.Sensitivity) * GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, Position.Y - (Settings.Sensitivity) * GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y);
                //Position = Vector2.Add(Position, Velocity);
                Rotation = (float)Math.Atan2((-GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y), GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) + (float)Math.PI / 2;
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Length() > Settings.Deadzone)
            {
                Rotation = (float)Math.Atan2((-GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y), GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X) + (float)Math.PI / 2;
            }


            if (GamePad.GetState(PlayerIndex.One).Buttons.RightStick == ButtonState.Pressed)
            {
                if (Math.Abs(Velocity.X) > 0 || Math.Abs(Velocity.Y) > 0)
                {
                    Velocity = new Vector2(0);
                }
            }



            Velocity = Vector2.Add(Velocity, Acceleration);

            MaxSpeed = 10;

            if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed && Vector2.Normalize(Velocity).Length() * MaxSpeed > 9.999)
            {
                MaxSpeed = 100;
            }

            if (Velocity.Length() > MaxSpeed)
            {
                Velocity = MaxSpeed * Vector2.Normalize(Velocity);
            }
                


            Position = Vector2.Add(Position, Velocity);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position: Position, rotation: Rotation, origin: new Vector2(8, 8));
        }
    }
}
