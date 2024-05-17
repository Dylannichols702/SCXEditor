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
    private SpriteFont _defaultFont;
    private List<RendererElement> _elements = new List<RendererElement>();

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
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Load Font
        _defaultFont = Content.Load<SpriteFont>("Fonts/DefaultFont");

        // Initialize RenderElements
        RendererElements.Track _leftTrack = new(450, 0, 200, 1000);
        RendererElements.Track _rightTrack = new(700, 0, 200, 1000);
        JudgmentLine _judgmentLine = new(400, 500, 550, 10);

        // Add RenderElements to the list
        _elements.Add(_leftTrack);
        _elements.Add(_rightTrack);
        _elements.Add(_judgmentLine);
    }

    protected override void Update(GameTime gameTime)
    {
        // TODO: Add your update logic here
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Microsoft.Xna.Framework.Color(96, 77, 128));

        _spriteBatch.Begin();
        foreach (RendererElement element in _elements)
        {
            element.Draw(ref _spriteBatch);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
