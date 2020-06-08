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
        public static Dictionary<int, List<Texture2D>> Textures;
        //public new Texture2D Texture { get; set; }

        //public static new List<KeyValuePair<int, Texture2D>> Texture; //int size, Texture2D texture

        //public static new Texture2D Texture { get; } = Textures[Random.Next(Textures.Count)]; //possibly redundant


        static Random Random = new Random();

        //int Size = default; // random number between 0 and exclusive upper bound (to be decided) sizes: 8x8, 16x16, 32x32, 64x64(?), 128x128(??)

        float RotationSpeed = (float)Random.NextDouble()/10; // random 7dp number between 0 and 0.1 & only generated once per instance of asteroid class ONLY CLOCKWISE ATM

        public Asteroid(int size, Player player) : base
            (
            rotation     : (float)Random.NextDouble()*(float)Math.PI*2, //initial rotation betweem 0 and 2PI in radians, not exactly 'necessary' so to speak
            acceleration : new Vector2(0, 0), 
            velocity     : new Vector2(Random.Next(-5, 5)+(float)Random.NextDouble(), Random.Next(-5, 5)+(float)Random.NextDouble()), //may need to pass in player argument for relative positioning of asteroids in base class argument 'position'
            position     : new Vector2(Random.Next(1920), Random.Next(1080)), 
            texture      : GetTexture(size)      //need to assign texture randomly from an array of possible textures (4 different textures for each size of asteroid?)
            )
        {
            //need to sort out base class arguments
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

            spriteBatch.Draw(Texture, position: Position, rotation: Rotation, origin: new Vector2(Texture.Width/2, Texture.Height/2));

        }

        private static Texture2D GetTexture(int size)
        {
            Textures.TryGetValue(size, out List<Texture2D> textures);
            Texture2D texture = textures[Random.Next(textures.Count)];

            return texture;
        }
    }
}
