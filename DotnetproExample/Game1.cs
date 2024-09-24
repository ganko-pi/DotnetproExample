using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;

namespace DotnetproExample;

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    None,
}

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private OrthographicCamera _camera;

    private TiledMap _tiledMap;
    private TiledMapRenderer _tiledMapRenderer;
    private Texture2D _playerTextureWalkUp;
    private Texture2D _playerTextureWalkDown;
    private Texture2D _playerTextureWalkLeft;
    private Texture2D _playerTextureWalkRight;
    private Player _player = new();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 1600;
        _graphics.PreferredBackBufferHeight = 900;
        _graphics.ApplyChanges();

        ViewportAdapter viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 320, 180);
        _camera = new OrthographicCamera(viewportAdapter);
        _camera.LookAt(_player.Position);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        // TODO: use this.Content to load your game content here
        _tiledMap = Content.Load<TiledMap>("Maps/map");
        _tiledMapRenderer = new(GraphicsDevice, _tiledMap);
        _playerTextureWalkUp = Content.Load<Texture2D>("Player/player_walk_up");
        _playerTextureWalkDown = Content.Load<Texture2D>("Player/player_walk_down");
        _playerTextureWalkLeft = Content.Load<Texture2D>("Player/player_walk_left");
        _playerTextureWalkRight = Content.Load<Texture2D>("Player/player_walk_right");

        Sprite[] playerAnimations =
        [
            new Sprite(_playerTextureWalkUp, 4, 8),
            new Sprite(_playerTextureWalkDown, 4, 8),
            new Sprite(_playerTextureWalkLeft, 4, 8),
            new Sprite(_playerTextureWalkRight, 4, 8),
        ];
        _player.Animations = playerAnimations;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _tiledMapRenderer.Update(gameTime);
        _player.Update(gameTime);
        _camera.LookAt(_player.Position);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        Matrix transformMatrix = _camera.GetViewMatrix();
        _tiledMapRenderer.Draw(transformMatrix);
        _spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
        _player.CurrentAnimation.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
