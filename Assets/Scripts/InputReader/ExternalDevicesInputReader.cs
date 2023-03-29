using System;
using Core.Services.Updater;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExternalDevicesInputReader: IEntityInputSource, IDisposable
{
    public float HorizontalDirection => Input.GetAxis("Horizontal");
    
    public bool Jump { get; private set; }
    
    public bool Attack { get; private set; }
    
    public ExternalDevicesInputReader()
    {
        ProjectUpdater.Instance.UpdateCalled += OnUpdate;
    }
    
    public void ResetOneTimeActions()
    {
        Jump = false;
        Attack = false;
    }

    public void Dispose() => ProjectUpdater.Instance.UpdateCalled -= OnUpdate;
    
    private bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();
    
    private void OnUpdate()
    {
        if (!IsPointerOverUI() && Input.GetButtonDown("Jump"))
        {
            Jump = true;
        }
        
        if (!IsPointerOverUI() && Input.GetButtonDown("Fire1"))
        {
            Attack = true;
        }
    }
}