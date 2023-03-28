using System;
using Core.Attack.Data;
using UnityEngine;

namespace Core.Attack.Controller
{
    [Serializable]
    public class Attacker
    {
        public bool IsAttack { get; private set; }
        
        [SerializeField] private AttackerData _attackerData;
        private float _remainingTimeAttack;
        
        public Attacker(AttackerData attackerData)
        {
            _attackerData = attackerData;
        }
        
        public void Attack()
        {
            IsAttack = true;
            _remainingTimeAttack = _attackerData.TimeAttack;
        }
        
        public void UpdateAttack()
        {
            if (!IsAttack)
                return;

            _remainingTimeAttack -= Time.deltaTime;

            if(_remainingTimeAttack <= 0)
                IsAttack = false;
        }
    }
}