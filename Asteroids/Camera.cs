using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Camera
    {
        public Matrix Transform { get; set; }

        Viewport Viewport;

        Vector2 Centre;

        public Camera(Viewport viewport)
        {
            Viewport = viewport;
        }

        public void Update(GameTime gameTime, Player player)
        {
            Centre = new Vector2(player.Position.X, player.Position.Y);

            Transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-Centre.X + Viewport.Width/2, -Centre.Y + Viewport.Height/2, 0));
        }
    }
}
