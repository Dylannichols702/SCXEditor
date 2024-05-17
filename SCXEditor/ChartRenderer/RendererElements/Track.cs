using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;

namespace SCXEditor.ChartRenderer.RendererElements
{
    public class Track : RendererElement
    {
        private Rectangle rect = new Rectangle();
        Microsoft.Xna.Framework.Color backgroundColor;
        Microsoft.Xna.Framework.Color lineColor;

        public Track(int x, int y, int width, int height)
        {
            rect.X = x;
            rect.Y = y;
            rect.Width = width;
            rect.Height = height;
            backgroundColor = new(0, 0, 0);
            lineColor = new(100, 100, 100);
        }

        public void Draw(ref SpriteBatch spriteBatch)
        {
            // Draw background
            spriteBatch.FillRectangle(rect, backgroundColor);

            // Draw separating lines
            spriteBatch.DrawLine(
                new Vector2(rect.X + rect.Width / 3, 0), 
                new Vector2(rect.X + rect.Width / 3, rect.Height), 
                lineColor);
            spriteBatch.DrawLine(
                new Vector2(rect.X + (2 * rect.Width / 3), 0),
                new Vector2(rect.X + (2 * rect.Width / 3), rect.Height),
                lineColor);
        }
    }
}
