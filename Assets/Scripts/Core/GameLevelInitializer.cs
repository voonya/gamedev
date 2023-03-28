using System;
using System.Collections.Generic;
using Core.Services.Updater;
using Player;
using UnityEngine;

namespace Core
{
    public class GameLevelInitializer : MonoBehaviour
    {
        [SerializeField]
        private PlayerEntity _playerEntity;

        [SerializeField] private GameUIInputView _gameUIInputView;

        private ExternalDevicesInputReader _externalDevicesInput;
        private PlayerSystem _playerSystem;
        private ProjectUpdater _projectUpdater;

        private List<IDisposable> _disposables;

        private void Awake()
        {
            _disposables = new List<IDisposable>();
            if(ProjectUpdater.Instance == null)
                _projectUpdater  = new GameObject().AddComponent<ProjectUpdater>();
            else
                _projectUpdater = ProjectUpdater.Instance as ProjectUpdater;

            _externalDevicesInput = new ExternalDevicesInputReader();
            _playerSystem = new PlayerSystem(_playerEntity, new List<IEntityInputSource>
            {
                _gameUIInputView,
                _externalDevicesInput
            });
            
            _disposables.Add(_externalDevicesInput);
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}