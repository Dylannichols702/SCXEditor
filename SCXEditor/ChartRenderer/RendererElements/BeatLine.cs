using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SCXEditor.ChartRenderer.RendererElements
{
    public class BeatLine : RendererElement
    {
        public const int TEXT_PADDING = 10;

        private int x1, x2, y1, y2;
        private Microsoft.Xna.Framework.Color color = new(153, 153, 153);
        private SpriteFont font;
        private int beat;

        public BeatLine(int x1, int x2, int y1, int y2, int beat, SpriteFont font)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
            this.beat = beat;
            this.font = font;
        }

        public void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(new Vector2(x1, y1), new Vector2(x2, y2), color);
            // TODO: Fix this functionality
            // spriteBatch.DrawString(font, beat.ToString(), new Vector2(x2 + TEXT_PADDING, y2 + TEXT_PADDING), color);
        }
    }
}
