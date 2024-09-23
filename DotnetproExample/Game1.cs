using System;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace DotnetproExample;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private OrthographicCamera _camera;

    private Texture2D _playerSprite;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        ViewportAdapter viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 320, 180);
        _camera = new OrthographicCamera(viewportAdapter);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        // TODO: use this.Content to load your game content here
        _playerSprite = GenerateRandomTexture(GraphicsDevice, 16, 16);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        Matrix transformMatrix = _camera.GetViewMatrix();
        _spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(_playerSprite, new Vector2(50, 50), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private Texture2D GenerateRandomTexture(GraphicsDevice graphicsDevice, int width, int height)
    {
        Random random = new();
        Color[] data = Enumerable.Range(0, width * height)
            .Select(_ => new Color(random.Next(256), random.Next(256), random.Next(256)))
            .ToArray();
        Texture2D texture = new(graphicsDevice, width, height);
        texture.SetData(data);
        return texture;
    }
}
