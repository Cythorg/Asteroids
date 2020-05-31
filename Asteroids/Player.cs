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

        public static Texture2D RedShip;
        public static Texture2D GreenShip;
        public static Texture2D BlueShip;
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


            if (Velocity.Length() > 10.01)
            //if (true)
            {

                //new Vector2(8+(float)Math.Cos((2/3)*Math.PI), 8 + (float)Math.Cos((2 / 3) * Math.PI));
                //Vector2 _120 = new Vector2(8, 8) + 3*new Vector2((float)Math.Cos((2*Math.PI/3) + (Math.PI/2)), (float)Math.Sin((2*Math.PI/3) - (Math.PI / 2)));
                //Vector2 _240 = new Vector2(8, 8) + 3*new Vector2((float)Math.Cos((4*Math.PI/3) + (Math.PI / 2)), (float)Math.Sin((4*Math.PI/3) - (Math.PI / 2)));
                //Vector2 _360 = new Vector2(8, 8) + 3*new Vector2((float)Math.Cos((6*Math.PI/3) + (Math.PI / 2)), (float)Math.Sin((6*Math.PI/3) - (Math.PI / 2)));

                Vector2 _120 = Position + 3 * new Vector2((float)Math.Cos((2 * Math.PI / 3) + (Math.PI / 2)), (float)Math.Sin((2 * Math.PI / 3) - (Math.PI / 2)));
                Vector2 _240 = Position + 3 * new Vector2((float)Math.Cos((4 * Math.PI / 3) + (Math.PI / 2)), (float)Math.Sin((4 * Math.PI / 3) - (Math.PI / 2)));
                Vector2 _360 = Position + 3 * new Vector2((float)Math.Cos((6 * Math.PI / 3) + (Math.PI / 2)), (float)Math.Sin((6 * Math.PI / 3) - (Math.PI / 2)));

                spriteBatch.Draw(RedShip, position: _240, rotation: Rotation, origin: new Vector2(8, 8));
                spriteBatch.Draw(GreenShip, position: _360, rotation: Rotation, origin: new Vector2(8, 8));
                spriteBatch.Draw(BlueShip, position: _120, rotation: Rotation, origin: new Vector2(8, 8));

                //spriteBatch.Draw(Texture, position: Position, rotation: Rotation, origin: new Vector2(8, 8));
            }

            else if (true)
            {
                spriteBatch.Draw(Texture, position: Position, rotation: Rotation, origin: new Vector2(8, 8));
            }




        }
    }
}
