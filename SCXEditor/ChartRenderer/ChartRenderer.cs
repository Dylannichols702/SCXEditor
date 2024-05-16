using Avalonia.Controls.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using System.Collections.Generic;
using SCXEditor.ChartRenderer.RendererElements;

namespace SCXEditor.ChartRenderer;

public class ChartRenderer : Game
{
    private List<RendererElement> _elements = new List<RendererElement>();

    private RendererElements.Track _leftTrack = new(450, 0, 200, 1000);
    private RendererElements.Track _rightTrack = new(700, 0, 200, 1000);

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public ChartRenderer()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "ChartRenderer/Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _elements.Add(_leftTrack);
        _elements.Add(_rightTrack);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Microsoft.Xna.Framework.Color(96, 77, 128));

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        foreach (RendererElement element in _elements)
        {
            element.Draw(ref _spriteBatch);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
