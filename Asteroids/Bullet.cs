using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Bullet : Projectile
    {
        public static new Texture2D Texture { get; set; }
        public Player Player { get; set; }
        public float BulletVelocity { get; set; }


        public Bullet(Player player, float bulletVelocity) : base(player.Rotation, Vector2.Zero, player.Velocity, player.Position, Texture)
        {
            Player = player;
            BulletVelocity = bulletVelocity;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 BulletVelocity = new Vector2((float)Math.Sin(Rotation), -(float)Math.Cos(Rotation)) * this.BulletVelocity;

            Velocity = Velocity + Accelleration;
            Position = Position + Velocity + BulletVelocity;

            spriteBatch.Draw(Texture, position: Position, rotation: Rotation, origin: new Vector2(2, 4));

        }
    }
}
