using System;
using Core.Enums;
using UnityEngine;

namespace Core.Movement.Data
{
    [Serializable]
    public class DirectionalMovementData
    {
        [field: SerializeField] public Direction Direction { get; private set; }
        [field: SerializeField] public float HorizontalSpeed { get; private set; }
    }
}