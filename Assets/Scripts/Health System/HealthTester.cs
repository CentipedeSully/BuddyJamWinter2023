using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTester : MonoBehaviour
{
    //Declarations
    [SerializeField] private HealthBehavior _healthReference;
    [SerializeField] private float _damageInputCooldownDuration = .5f;
    private bool _isDamageInputReady = true;
    [SerializeField] private float _healInputCooldownDuration = .5f;
    private bool _isHealInputReady = true;

    //monobehavoirs
    private void Update()
    {
        if (_healthReference != null)
        {
            TakeDamageOnInputQ();
            HealDamageOnInputE();
        }
    }


    //utilites
    private void ReadyDamageInput()
    {
        _isDamageInputReady = true;
    }

    private void ReadyHealInput()
    {
        _isHealInputReady = true;
    }

    private void TakeDamageOnInputQ()
    {
        if (_isDamageInputReady && Input.GetKeyDown(KeyCode.Q))
        {
            _healthReference.DamageHealth(1);
            _isDamageInputReady = false;
            Invoke("ReadyDamageInput", _damageInputCooldownDuration);
        }
    }

    private void HealDamageOnInputE()
    {
        if (_isHealInputReady && Input.GetKeyDown(KeyCode.E))
        {
            _healthReference.HealHealth(1);
            _isHealInputReady = false;
            Invoke("ReadyHealInput", _healInputCooldownDuration);
        }
    }


}
