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
    private FMOD.Studio.EventInstance instance;

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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(_validTargetTag))
            collision.gameObject.GetComponent<IDamagable>().TakeDamageAndKnockBack(_contactDamage, transform);
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
            if (_target != null && PlayerObjectManager.Instance.IsPlayerAlive())
                _moveReference.SetMoveDirection((_target.transform.position - transform.position).normalized);
            else if (PlayerObjectManager.Instance.IsPlayerAlive() == false)
                Deaggro();
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
        else if (_target == null)
            Deaggro();
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
        if (_guardPosition != null)
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

    //SFX
    public void Enemies_BlanketIdleFootseps_Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Enemies/Enemy_Blanket/Enemy_BlanketIdleFootsteps");
        instance.start();
    }

    public void Enemies_BlanketIdleFootsteps_Stop()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
    }

    public void Enemies_BunIdleFootseps_Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Enemies/Enemy_Bun/Enemy_BunIdleFootsteps");
        instance.start();
    }

    public void Enemies_BunIdleFootsteps_Stop()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
    }

    public void Enemies_TedBearIdleFootseps_Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Enemies/Enemy_TedBear/Enemy_TedBearIdleFootsteps");
        instance.start();
    }

    public void Enemies_TedBearIdleFootsteps_Stop()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
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

    public void SetGuardPosition(Transform position)
    {
        _guardPosition = position;
    }
}
