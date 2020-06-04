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
        public static Texture2D BlueShip; //not a great way of doing textures I must admit, will try to figure something out
        public Vector2 Position { get; set; } = new Vector2(0, 0); //sets player to 0,0 on initalisation
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public float Rotation { get; set; }
        public Viewport Viewport { get; set; }

        int MaxSpeed = 10; //needs refactoring


        public Player()
        {
        }

        public void Update()
        {
            Acceleration = new Vector2(0, 0);

            //Rotation = (float)Math.Atan2(-(Position.X - Mouse.GetState().X), Position.Y - Mouse.GetState().Y);  //sets player rotation to be towards the cursor, will uncomment when inputs are worked on

            if (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed) //resets player to 0,0 and removes any residual speed
            {
                Position = new Vector2(0, 0);
                Velocity = new Vector2(0, 0);
            }

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
            {
                Vector2 _120 = Position + Velocity.Length()/10 * new Vector2((float)Math.Cos((2 * Math.PI / 3) + (Math.PI / 2)), (float)Math.Sin((2 * Math.PI / 3) - (Math.PI / 2)));
                Vector2 _360 = Position + Velocity.Length()/10 * new Vector2((float)Math.Cos((6 * Math.PI / 3) + (Math.PI / 2)), (float)Math.Sin((6 * Math.PI / 3) - (Math.PI / 2)));
                Vector2 _240 = Position + Velocity.Length()/10 * new Vector2((float)Math.Cos((4 * Math.PI / 3) + (Math.PI / 2)), (float)Math.Sin((4 * Math.PI / 3) - (Math.PI / 2)));

                spriteBatch.Draw(RedShip, position: _240, rotation: Rotation, origin: new Vector2(8, 8));                     //      v G
                spriteBatch.Draw(GreenShip, position: _360, rotation: Rotation, origin: new Vector2(8, 8));                   //      R ^ B         RGB is the order of subpixels from left to right, hence the order _360 is also 0 which is left
                spriteBatch.Draw(BlueShip, position: _120, rotation: Rotation, origin: new Vector2(8, 8));                    //          ^
            }

            else if (true)
            {
                spriteBatch.Draw(Texture, position: Position, rotation: Rotation, origin: new Vector2(8, 8));
            }




        }
    }
}
