using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehavior : MonoBehaviour
{
    //Declarations
    [Header("Health Settings")]
    [SerializeField] private bool _isHealthFullOnStart = true;
    [SerializeField] private int _healthMax = 3;
    [SerializeField] private int _healthCurrent = 3;

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


    //Utilites
    public void DamageHealth(int value)
    {
        if (value >= 0 && _healthCurrent > 0)
        {
            _healthCurrent -= value;
            Mathf.Clamp(_healthCurrent, 0, _healthMax);
            OnDamaged?.Invoke();

            if (_healthCurrent == 0)
                OnDeath?.Invoke();
        }
    }

    public void HealHealth(int value)
    {
        if (value >= 0 && _healthCurrent < _healthMax)
        {
            _healthCurrent += value;
            Mathf.Clamp(_healthCurrent, 0, _healthMax);
            OnHealed?.Invoke();
        }
    }


    //Getters and Setters
    public void SetCurrentHealth(int value)
    {
        if (value >= 0)
        {
            _healthCurrent = value;
            Mathf.Clamp(_healthCurrent, 0, _healthMax);
        }
    }

    public void SetMaxHealth(int value)
    {
        if (value >= 0)
        {
            _healthMax = value;
            Mathf.Clamp(_healthCurrent, 0, _healthMax);
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
