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
        public Texture2D ship;
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public float Rotation { get; set; }
        public Viewport Viewport { get; set; }


        public Player()
        {
        }

        public void Update()
        {
            Acceleration = new Vector2(0, 0);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed)
            {
                Position = new Vector2(Viewport.Width / 2, Viewport.Height / 2);
                Velocity = new Vector2(0, 0);
            }
            if (Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y) > Settings.Deadzone || Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) > Settings.Deadzone)
            {
                Acceleration = new Vector2(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, -GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y);
                //Velocity = Vector2.Add(Velocity, Acceleration);
                //Position = new Vector2(Position.X + (Settings.Sensitivity) * GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, Position.Y - (Settings.Sensitivity) * GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y);
                //Position = Vector2.Add(Position, Velocity);
                Rotation = (float)Math.Atan2((-GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y), GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) + (float)Math.PI / 2;
            }
            Velocity = Vector2.Add(Velocity, Acceleration);
            Position = Vector2.Add(Position, Velocity);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ship, position: Position, rotation: Rotation, origin: new Vector2(8, 8));
        }
    }
}
