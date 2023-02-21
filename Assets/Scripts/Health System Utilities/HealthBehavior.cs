using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehavior : MonoBehaviour, IDamagable
{
    //Declarations
    [Header("Health Settings")]
    [SerializeField] private bool _isHealthFullOnStart = true;
    [SerializeField] private bool _isHealthDisplaySet = false;
    [SerializeField] private IDisplayable _healthDisplayBehaviorRef;
    [SerializeField] private int _healthMax = 3;
    [SerializeField] private int _healthCurrent = 3;
    [SerializeField] private float _invulnerabilityDuration = .5f;
    [SerializeField] private bool _isInvulnerable = false;
 
    [Header("Events")]
    public UnityEvent OnDamaged;
    public UnityEvent OnHealed;
    public UnityEvent OnDeath;



    //Monbehaviors
    private void Awake()
    {
        if (_isHealthFullOnStart)
            _healthCurrent = _healthMax;
    }


    //Interface
    public void TakeDamage(int value)
    {
        DamageHealth(value);
    }

    //Utilites
    public void DamageHealth(int value)
    {
        if (value >= 0 && _healthCurrent > 0 && !_isInvulnerable)
        {
            _healthCurrent -= value;
            Mathf.Clamp(_healthCurrent, 0, _healthMax);
            if (_isHealthDisplaySet)
                _healthDisplayBehaviorRef.UpdateGUIDisplay();

            OnDamaged?.Invoke();

            if (_healthCurrent == 0)
                OnDeath?.Invoke();
            else
            {
                _isInvulnerable = true;
                Invoke("EndInvulnerability", _invulnerabilityDuration);
            }
        }
    }

    public void HealHealth(int value)
    {
        if (value >= 0 && _healthCurrent < _healthMax)
        {
            _healthCurrent += value;
            Mathf.Clamp(_healthCurrent, 0, _healthMax);
            if (_isHealthDisplaySet)
                _healthDisplayBehaviorRef.UpdateGUIDisplay();
            OnHealed?.Invoke();
        }
    }

    private void EndInvulnerability()
    {
        _isInvulnerable = false;
    }

    //Getters and Setters
    public void SetCurrentHealth(int value)
    {
        if (value >= 0)
        {
            _healthCurrent = value;
            Mathf.Clamp(_healthCurrent, 0, _healthMax);
            if (_isHealthDisplaySet)
                _healthDisplayBehaviorRef.UpdateGUIDisplay();
        }
    }

    public void SetMaxHealth(int value)
    {
        if (value >= 0)
        {
            _healthMax = value;
            Mathf.Clamp(_healthCurrent, 0, _healthMax);
            if (_isHealthDisplaySet)
                _healthDisplayBehaviorRef.UpdateGUIDisplay();
        }
    }

    public int GetCurrentHealth()
    {
        return _healthCurrent;
    }

    public int GetMaxHealth()
    {
        return _healthMax;
    }

    public bool IsInvunlerable()
    {
        return _isInvulnerable;
    }

    public void SetInvulnerability(bool value)
    {
        _isInvulnerable = value;
    }

    public void SetInvulerabilityDuration(float value)
    {
        if (value >= 0)
            _invulnerabilityDuration = value;
    }

    public float GetInvulnerabilityDuration()
    {
        return _invulnerabilityDuration;
    }

    public void SetHealthDisplayBehavior(IDisplayable newDisplayBehavior)
    {
        _healthDisplayBehaviorRef = newDisplayBehavior;
        _isHealthDisplaySet = true;

    }


    //Logs
    public void LogDamageTaken()
    {
        Debug.Log("Oof! Damage Received on " + gameObject.name + "!");
    }

    public void LogHeals()
    {
        Debug.Log("Heals Gained on " + gameObject.name + "!");
    }

    public void LogDeath()
    {
        Debug.Log(gameObject.name + " died!");
    }

}
