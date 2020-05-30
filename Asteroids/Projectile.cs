using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Projectile
    {
        public Texture2D Texture { get; set; }
        public float Rotation { get; set; }
        public Vector2 Accelleration { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }
        public bool Dead { get; set; } = false;



        public Projectile(float rotation, Vector2 accelleration, Vector2 velocity, Vector2 position, Texture2D texture)
        {
            Rotation = rotation;
            Accelleration = accelleration;
            Velocity = velocity;
            Position = position;
            Texture = texture;

        }

        //public Projectile(float rotation, Vector2 velocity, Vector2 position, Texture2D texture)
        //{
        //    Rotation = rotation;
        //    Velocity = velocity;
        //    Position = Vector2.Add(position, player.Velocity);
        //    Texture = texture;

        //}

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //Vector2 ProjectileVelocity = new Vector2((float)Math.Sin(Rotation),-(float)Math.Cos(Rotation))*6;

            Velocity = Velocity + Accelleration;
            Position = Position + Velocity;
            //Position = Position + Velocity + ProjectileVelocity;


            spriteBatch.Draw(Texture, position: Position, rotation: Rotation, origin: new Vector2(2, 4));

        }
    }
}
