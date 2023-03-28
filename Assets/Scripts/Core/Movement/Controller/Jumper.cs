using System;
using Core.Movement.Data;
using UnityEngine;

namespace Core.Movement.Controller
{
    [Serializable]
    public class Jumper
    {
        public bool IsJumping { get { return _rigidbody.velocity.y > 0; }}
        public bool IsFalling { get { return _rigidbody.velocity.y < 0; } }
        
        private JumperData _jumperData;
        private Rigidbody2D _rigidbody;
        
        public Jumper(Rigidbody2D rigidbody, JumperData jumperData)
        {
            _rigidbody = rigidbody;
            _jumperData = jumperData;
        }
        
        public void Jump()
        {
            if (IsJumping || IsFalling)
            {
                return;
            }

            _rigidbody.AddForce(Vector2.up * _jumperData.JumpForce);
            _rigidbody.gravityScale = _jumperData.GravityScale;
        }
    }
}