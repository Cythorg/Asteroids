using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.Content
{
    class Asteroid : Projectile
    {
        public static new Texture2D Texture { get; set; }

        static Random Random = new Random();

        int Size = Random.Next(1,5); // random number between 1 and 4
        float RotationSpeed = (float)Random.NextDouble()/10; // random number to 7dp between 0 and 1

        public Asteroid() : base((float)Random.NextDouble()*2*(float)Math.PI, new Vector2(0, 0), new Vector2(Random.Next(-5, 5)+(float)Random.NextDouble(), Random.Next(-5, 5)+(float)Random.NextDouble()), new Vector2(Random.Next(1920), Random.Next(1080)), Texture) //asteroid: size between 1-10?  base: rotation[between 0 and 2PI], acceleration[vector2], velocity[vector2], position[vector2], texture
        {
            //need to sort out base class arguments
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rotation += RotationSpeed;

            Velocity = Velocity + Accelleration;
            Position = Position + Velocity;

            spriteBatch.Draw(Texture, position: Position, rotation: Rotation, origin: new Vector2(8, 8));

        }
    }
}
