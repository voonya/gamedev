using Core.Animation;
using Core.Attack.Controller;
using Core.Attack.Data;
using Core.Movement.Controller;
using Core.Movement.Data;
using Core.Tools;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private AnimationController _animationController;
        
        [SerializeField] private DirectionalMovementData _directionalMovementData;
        private DirectionalMover _directionalMover;
        
        [SerializeField] private JumperData _jumperData;
        private Jumper _jumper;

        [SerializeField] private DirectionalCameraPair _cameras;

        [SerializeField] private AttackerData _attackerData;
        private Attacker _attacker;

        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _directionalMover = new DirectionalMover(_rigidbody, _directionalMovementData);
            _jumper = new Jumper(_rigidbody, _jumperData);
            _attacker = new Attacker(_attackerData);
        }

        private void Update()
        {
            UpdateAnimations();
            UpdateCameras();
            _attacker.UpdateAttack();
        }

        public void Attack() => _attacker.Attack();

        public void MoveHorizontally(float direction) => _directionalMover.MoveHorizontally(direction);

        public void Jump() => _jumper.Jump();
        
        private void UpdateAnimations()
        {
            _animationController.PlayAnimation(AnimationType.Idle, true);
            _animationController.PlayAnimation(AnimationType.Walk, _directionalMover.IsMoving);
            _animationController.PlayAnimation(AnimationType.Jump, _jumper.IsJumping);
            _animationController.PlayAnimation(AnimationType.Fall, _jumper.IsFalling);
            _animationController.PlayAnimation(AnimationType.Attack, _attacker.IsAttack);
        }
        
        private void UpdateCameras()
        {
            foreach (var cameraPair in _cameras.DirectionalCameras)
            {
                cameraPair.Value.enabled = cameraPair.Key == _directionalMover.Direction;
            }
        }
    }
}

