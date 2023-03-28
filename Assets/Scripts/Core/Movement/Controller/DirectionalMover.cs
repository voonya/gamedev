using Core.Enums;
using Core.Movement.Data;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class DirectionalMover
    {
        public Direction Direction { get; private set; }
        public bool IsMoving => _movement.magnitude > 0;
        
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _transform;
        private readonly DirectionalMovementData _directionalMovementData;

        private Vector2 _movement;

        public DirectionalMover(Rigidbody2D rigidbody, DirectionalMovementData directionalMovementData)
        {
            _rigidbody = rigidbody;
            _transform = rigidbody.transform;
            _directionalMovementData = directionalMovementData;
        }
        public void MoveHorizontally(float direction)
        {
            _movement.x = direction;
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _directionalMovementData.HorizontalSpeed;
            _rigidbody.velocity = velocity;
            SetDirection(direction);
        }
        
        private void SetDirection(float direction)
        {
            if (Direction == Direction.Right && direction < 0 || Direction == Direction.Left && direction > 0)
            {
                Flip();    
            }
        }

        private void Flip()
        {
            _transform.Rotate(0, 180, 0);
            Direction = Direction == Direction.Right ? Direction.Left : Direction.Right;
        }
    }
}