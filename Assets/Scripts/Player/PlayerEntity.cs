using System;
using System.Collections;
using System.Collections.Generic;
using Core.Enums;
using Core.Tools;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        [Header("HorizontalMovement")]
        [SerializeField] private Direction _direction;
        [SerializeField] private float _horizontalSpeed;

        [Header("Jump")] 
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravityScale;

        [SerializeField] private DirectionalCameraPair _cameras;
        
        private Rigidbody2D _rigidbody;
        private CapsuleCollider2D _collider;

        private Vector2 _movement;
        private AnimationType _currentAnimationType;

        private bool _isAttack;
        
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            UpdateAnimations();
        }

        private void UpdateAnimations()
        {
            PlayAnimation(AnimationType.Idle, true);
            PlayAnimation(AnimationType.Walk, _movement.magnitude > 0);
            PlayAnimation(AnimationType.Jump, _rigidbody.velocity.y != 0);
            //PlayAnimation(AnimationType.Attack, _isAttack);
        }

        public void Jump()
        {
            if (_rigidbody.velocity.y != 0)
            {
                return;
            }
            
            _rigidbody.AddForce(Vector2.up * _jumpForce);
            _rigidbody.gravityScale = _gravityScale;
        }

        public void Attack()
        {
           PlayAnimation(AnimationType.Attack, true);
        }

        public void ResetAtack()
        {
            _isAttack = false;
        }

        public void MoveHorizontally(float direction)
        {
            _movement.x = direction;
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _horizontalSpeed;
            _rigidbody.velocity = velocity;
            SetDirection(direction);
        }

        private void SetDirection(float direction)
        {
            if (_direction == Direction.Right && direction < 0 || _direction == Direction.Left && direction > 0)
            {
                Flip();    
            }
        }

        private void Flip()
        {
            transform.Rotate(0, 180, 0);
            _direction = _direction == Direction.Right ? Direction.Left : Direction.Right;
            foreach(var pair in _cameras.DirectionalCameras)
            {
                pair.Value.enabled = pair.Key == _direction;
            }
        }

        private void PlayAnimation(AnimationType animationType, bool active)
        {
            if (!active)
            {
                if (_currentAnimationType == AnimationType.Idle || _currentAnimationType != animationType)
                    return;

                _currentAnimationType = AnimationType.Idle;
                PlayAnimation(_currentAnimationType);
                return;
            }

            if (_currentAnimationType >= animationType)
                return;

            _currentAnimationType = animationType;
            PlayAnimation(_currentAnimationType);
        }

        private void PlayAnimation(AnimationType animationType)
        {
            _animator.SetInteger(nameof(AnimationType), (int)animationType);
        }
    }
}

