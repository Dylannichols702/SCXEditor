using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCXEditor.ChartRenderer.RendererElements
{
    public class JudgmentLine : RendererElement
    {
        private Rectangle rect = new Rectangle();
        private Microsoft.Xna.Framework.Color color = new(200, 200, 200);

        public JudgmentLine(int x, int y, int width, int height)
        {
            rect.X = x; 
            rect.Y = y; 
            rect.Width = width; 
            rect.Height = height;
        }

        public void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(
                new Vector2(rect.X, rect.Y), 
                new Vector2(rect.Width, rect.Height), 
                color);
        }
    }
}
