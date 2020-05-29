using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Projectile
    {
        public Texture2D Texture;
        public float Rotation;
        public Vector2 Velocity;
        public Vector2 Position;



        public Projectile(Player player, Texture2D texture)
        {
            Rotation = player.Rotation;
            Velocity = player.Velocity;
            Position = player.Position;
            Texture = texture;

        }

        //public Projectile(float rotation, Vector2 velocity, Vector2 position, Texture2D texture)
        //{
        //    Rotation = rotation;
        //    Velocity = velocity;
        //    Position = Vector2.Add(position, player.Velocity);
        //    Texture = texture;

        //}

        public void Draw(SpriteBatch spriteBatch)
        {

            Vector2 AddVelocity = new Vector2((float)Math.Sin(Rotation),-(float)Math.Cos(Rotation));
            Vector2 TempVelocity = Vector2.Add(AddVelocity*6, Velocity);

            Position = Vector2.Add(Position, TempVelocity);

            spriteBatch.Draw(Texture, position: Position, rotation: Rotation, origin: new Vector2(2, 4));

        }
    }
}
