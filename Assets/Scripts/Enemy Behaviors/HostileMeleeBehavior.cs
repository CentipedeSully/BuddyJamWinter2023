using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HostileMeleeBehavior : MonoBehaviour
{
    //Declarations
    [SerializeField] private int _contactDamage = 1;
    [SerializeField] private bool _isAggroed = false;
    [SerializeField] private float _aggroRange = 4;
    [SerializeField] private GameObject _target;
    [SerializeField] private string _validTargetTag = "Player";
    [SerializeField] private Transform _guardPosition;
    private MoveObject _moveReference;

    [Header("Events")]
    public UnityEvent OnAggroEntered;
    public UnityEvent OnAggroExited;


    //Monbehaviors
    private void Awake()
    {
        _moveReference = GetComponent<MoveObject>();
    }

    private void Update()
    {
        ManageHostileBehavior();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(_validTargetTag))
            collision.gameObject.GetComponent<IDamagable>().TakeDamage(_contactDamage);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _aggroRange);
    }
    


    //Utilites
    private void ManageHostileBehavior()
    {
        if (!_isAggroed)
        {
            AggroIfValidTargetIsInVicinity();
            MoveToGuardPosition();
        }
           

        else
        {
            _moveReference.SetMoveDirection((_target.transform.position - transform.position).normalized);
            DeaggroIfTargetIstooFarAway();
        }
    }

    private void DeaggroIfTargetIstooFarAway()
    {
        if (_target != null)
        {
            float targetDistance = Vector2.Distance(transform.position, _target.transform.position);

            if (targetDistance > _aggroRange)
                Deaggro();
        }
    }

    private void AggroIfValidTargetIsInVicinity()
    {
        Collider2D[] detectedColliders =  Physics2D.OverlapCircleAll(transform.position, _aggroRange);

        foreach (Collider2D foundCollider in detectedColliders)
        {
            if (foundCollider.CompareTag(_validTargetTag))
            {
                Aggro(foundCollider.gameObject);
                break;
            }
        }
    }

    private void MoveToGuardPosition()
    {
        _moveReference.SetMoveDirection((_guardPosition.position - transform.position).normalized);
    }

    public void Aggro(GameObject targetObject)
    {
        if (targetObject != null)
        {
            _target = targetObject;
            _isAggroed = true;
            OnAggroEntered?.Invoke();
        }    
    }

    public void Deaggro()
    {
        _target = null;
        _isAggroed = false;
        _moveReference.SetMoveDirection(Vector3.zero);
        OnAggroExited?.Invoke();
    }

    //Getters and Setters
    public bool IsAggroed()
    {
        return _isAggroed;
    }

    public float GetAggroRange()
    {
        return _aggroRange;
    }

    public void SetAggroRange(float newDistance)
    {
        if (newDistance >= 0)
            _aggroRange = newDistance;
    }
}
