using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DotnetproExample;

public class Player
{
    private Vector2 _position = new(50, 100);
    private int _speed = 85;
    private Direction _movementDirection = Direction.Down;
    private Sprite[] _animations;

    public Sprite CurrentAnimation { get; private set; }
    public Sprite[] Animations
    {
        get => _animations;
        set
        {
            _animations = value;

            foreach (Sprite animation in _animations)
            {
                animation.Position = _position;
            };
            
            CurrentAnimation = _animations[0];
            SetAnimation(movementDirectionChanged: true);
        }
    }

    public Vector2 Position { get => _position; }

    public void Update(GameTime gameTime)
    {
        Direction previousMovementDirection = _movementDirection;
        _movementDirection = GetNewMovementDirection();
        bool movementDirectionChanged = previousMovementDirection != _movementDirection;
        MovePlayer(gameTime);
        SetAnimation(movementDirectionChanged);
        UpdateAnimation(gameTime);
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

    private void SetAnimation(bool movementDirectionChanged)
    {
        if (_movementDirection == Direction.None)
        {
            CurrentAnimation.FrameIndex = 1;
            return;
        }

        CurrentAnimation = _animations[(int)_movementDirection];
        CurrentAnimation.Position = new Vector2(_position.X - (CurrentAnimation.Size.X - CurrentAnimation.Origin.X), _position.Y - (CurrentAnimation.Size.Y - CurrentAnimation.Origin.Y));

        if (movementDirectionChanged)
        {
            CurrentAnimation.FrameIndex = 0;
        }
    }

    private void UpdateAnimation(GameTime gameTime)
    {
        if (_movementDirection == Direction.None)
        {
            return;
        }

        CurrentAnimation.Update(gameTime);
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

        _position.Round();
    }
}
