using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace SCXEditor.ChartRenderer.RendererElements
{
    public interface RendererElement
    {
        void Draw(ref SpriteBatch spriteBatch);
    }
}
