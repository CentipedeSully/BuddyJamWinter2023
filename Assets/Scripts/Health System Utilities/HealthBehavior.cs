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
    [SerializeField] private IDeathBehavior _deathBehavior;
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

        _deathBehavior = GetComponent<IDeathBehavior>();
    }


    //Interface
    public void TakeDamage(int value)
    {
        DamageHealth(value);
    }

    public void TakeDamageAndKnockBack(int value, Transform damageOrigin)
    {
        if (_isInvulnerable == false)
            KnockBackSelf(damageOrigin, value * 6500);
        DamageHealth(value);
    }
    //Utilites
    public void DamageHealth(int value)
    {
        if (value >= 0 && _healthCurrent > 0 && !_isInvulnerable)
        {
            _healthCurrent -= value;
            _healthCurrent = Mathf.Clamp(_healthCurrent, 0, _healthMax);

            if (_isHealthDisplaySet)
                _healthDisplayBehaviorRef.UpdateGUIDisplay();

            OnDamaged?.Invoke();

            if (_healthCurrent == 0)
            {
                _deathBehavior.EnterDeathSequence();   
                OnDeath?.Invoke();
            }
                
            else
            {
                _isInvulnerable = true;
                Invoke("EndInvulnerability", _invulnerabilityDuration);
            }
        }
    }

    public void KnockBackSelf( Transform damageOrigin,float forceMagnitude)
    {
        Vector3 knockBackDirection = (transform.position - damageOrigin.position).normalized;
        //Debug.Log("Knockback direction: " + knockBackDirection);
        GetComponent<Rigidbody2D>().AddForce(knockBackDirection * forceMagnitude * Time.deltaTime, ForceMode2D.Impulse);
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

    //SFX
    
    public void Player_AttackHit()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Player_Attack/Player_AttackHit");
    }

    public void Enemies_BlanketPain()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Enemies/Enemy_Blanket/Enemy_BlanketPain");
    }

    public void Enemies_BunPain()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Enemies/Enemy_Bun/Enemy_BunPain");
    }

    public void Enemies_TedbearPain()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Enemies/Enemy_TedBear/Enemy_TedBearPain");
    }

    public void Enemies_BlanketDeath()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Enemies/Enemy_Blanket/Enemy_BlanketDeath");
    }

    public void Enemies_BunDeath()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Enemies/Enemy_Bun/Enemy_BunDeath");
    }

    public void Enemies_TedBearDeath()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Enemies/Enemy_TedBear/Enemy_TedBearDeath");
    }

    public void Player_Pain()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Player_Pain");
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
