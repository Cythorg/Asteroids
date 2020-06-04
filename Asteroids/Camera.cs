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

        public Camera(Viewport viewport)
        {
            Viewport = viewport;
        }

        public void Update(Player player)
        {
            Transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-player.Position.X + Viewport.Width/2, -player.Position.Y + Viewport.Height/2, 0));
        }
    }
}
