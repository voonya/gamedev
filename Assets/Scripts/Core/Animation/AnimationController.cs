using System;
using Player;
using UnityEngine;

namespace Core.Animation
{
    [Serializable]
    public class AnimationController
    {
        [SerializeField] private Animator _animator;
        private AnimationType _currentAnimationType;
        
        public void PlayAnimation(AnimationType animationType, bool active)
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