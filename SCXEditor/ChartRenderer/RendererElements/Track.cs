using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;

namespace SCXEditor.ChartRenderer.RendererElements
{
    public class Track : RendererElement
    {
        private Rectangle rect = new Rectangle();
        Microsoft.Xna.Framework.Color color;

        public int X
        {
            get { return rect.X; }
            set { rect.X = value; }
        }

        public int Y
        {
            get { return rect.Y; }
            set { rect.Y = value; }
        }

        public Track(int x, int y, int width, int height)
        {
            rect.X = x;
            rect.Y = y;
            rect.Width = width;
            rect.Height = height;
            color = new Microsoft.Xna.Framework.Color(0, 0, 0);
        }

        public void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(rect, color);
        }
    }
}
