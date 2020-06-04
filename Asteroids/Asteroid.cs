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

        int Size = Random.Next(1,1); // random number between 1 and exclusive upper bound (to be decided)
        float RotationSpeed = (float)Random.NextDouble()/10; // random 7dp number between 0 and 0.1

        public Asteroid(Player player) : base((float)Random.NextDouble()*2*(float)Math.PI, new Vector2(0, 0), new Vector2(Random.Next(-5, 5)+(float)Random.NextDouble(), Random.Next(-5, 5)+(float)Random.NextDouble()), new Vector2(Random.Next(1920), Random.Next(1080)), Texture) //asteroid: size between 1-10?  base: rotation[between 0 and 2PI], acceleration[vector2], velocity[vector2], position[vector2], texture
        {


            //need to sort out base class arguments
            //may need to pass in player argument for relative positioning of asteroids in base class argument 'position'
            //need to assign texture randomly from an array of possible textures (3 different textures for each size of asteroid?)
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (false) // need to decide on death condition for asteroids, not implemented 
            {
                Dead = true;
            }




            Rotation += RotationSpeed;

            Velocity = Velocity + Acceleration;
            Position = Position + Velocity;

            spriteBatch.Draw(Texture, position: Position, rotation: Rotation, origin: new Vector2(8, 8));

        }
    }
}
