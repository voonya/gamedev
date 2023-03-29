using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services.Updater;

namespace Player
{
    public class PlayerBrain : IDisposable
    {
        private readonly PlayerEntity _playerEntity;
        private readonly List<IEntityInputSource> _inputSources;
        
        public PlayerBrain(PlayerEntity playerEntity, List<IEntityInputSource> inputSources)
        {
            _playerEntity = playerEntity;
            _inputSources = inputSources;
            ProjectUpdater.Instance.FixedUpdateCalled += OnFixedUpdate;
        }

        public void Dispose()
        {
            ProjectUpdater.Instance.FixedUpdateCalled -= OnFixedUpdate;
        }
        
        private void OnFixedUpdate()
        {
            _playerEntity.MoveHorizontally(GetHorizontalDirection());
            if (IsJump)
                _playerEntity.Jump();

            if (IsAttack)
                _playerEntity.Attack();

            foreach (var source in _inputSources)
            {
                source.ResetOneTimeActions();
            }
        }

        private float GetHorizontalDirection()
        {
            foreach (var source in _inputSources)
            {
                if(source.HorizontalDirection == 0)
                    continue;

                return source.HorizontalDirection;
            }

            return 0;
        }
        
        private bool IsJump => _inputSources.Any(source => source.Jump);
        private bool IsAttack => _inputSources.Any(source => source.Attack);
    }
}