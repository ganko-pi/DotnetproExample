using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DotnetproExample;

public class Player
{
    private Vector2 _position = new(50, 50);
    private int _speed = 200;
    private Direction _movementDirection = Direction.Right;

    public Vector2 Position { get => _position; }

    public void Update(GameTime gameTime)
    {
        _movementDirection = GetNewMovementDirection();
        MovePlayer(gameTime);
    }

    private Direction GetNewMovementDirection()
    {
        KeyboardState keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Up))
        {
            return Direction.Up;
        }

        if (keyboardState.IsKeyDown(Keys.Down))
        {
            return Direction.Down;
        }

        if (keyboardState.IsKeyDown(Keys.Left))
        {
            return Direction.Left;
        }

        if (keyboardState.IsKeyDown(Keys.Right))
        {
            return Direction.Right;
        }

        return Direction.None;
    }

    private void MovePlayer(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        switch (_movementDirection)
        {
            case Direction.Up:
                _position.Y -= _speed * deltaTime;
                break;
            case Direction.Down:
                _position.Y += _speed * deltaTime;
                break;
            case Direction.Left:
                _position.X -= _speed * deltaTime;
                break;
            case Direction.Right:
                _position.X += _speed * deltaTime;
                break;
            case Direction.None:
            default:
                break;
        }
    }
}
