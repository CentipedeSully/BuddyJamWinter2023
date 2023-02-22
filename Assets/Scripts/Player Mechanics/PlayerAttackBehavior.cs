using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackBehavior : MonoBehaviour
{
    //Declarations
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRadius;
    [SerializeField] private bool _isAttackReady = true;
    [SerializeField] private bool _isAttacking = false;
    [SerializeField] private float _attackDuration = .5f;
    [SerializeField] private float _attackCooldown = 1f;
    private float _currentAttackDuration = 0;

    [SerializeField] private List<string> _validTargets;

    [Header("Events")]
    public UnityEvent OnAttackTriggered;


    //Monobehaviors
    private void Update()
    {
        ManageAttackState();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }


    //Utilites
    private void ReadyAttack()
    {
        _isAttackReady = true;
    }

    private void DamageTargetsInAttackRange()
    {
        Collider2D[] detectedColliders = Physics2D.OverlapCircleAll(transform.position, _attackRadius);

        foreach (Collider2D collider in detectedColliders)
        {
            if (_validTargets.Contains(collider.tag))
            {
                IDamagable otherDamageHandler = collider.GetComponent<IDamagable>();
                if (otherDamageHandler != null)
                    otherDamageHandler.TakeDamageAndKnockBack(_damage, transform);
            }
        }
    }

    public void EnterAttack()
    {
        if (_isAttackReady)
        {
            _isAttacking = true;
            _isAttackReady = false;
            OnAttackTriggered?.Invoke();
        }
    }

    private void ManageAttackState()
    {
        if (_isAttacking)
        {
            DamageTargetsInAttackRange();
            _currentAttackDuration += Time.deltaTime;

            if (_currentAttackDuration >= _attackDuration)
            {
                _isAttacking = false;
                Invoke("ReadyAttack", _attackCooldown);
            }
        }
    }
}
