using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnxietyBehavior : MonoBehaviour ,IDamagable
{
    //Declarations
    [SerializeField] private GameObject _guardPosition;
    [SerializeField] private GameObject _target;
    [SerializeField] private float _fleeRadius = 5;
    [SerializeField] private AnxietyAnimController _animController;
    [SerializeField] private SpriteRenderer _spriteRendererRef;
    private MoveObject _moveRef;

    [SerializeField] private float _attackRadius = 5;

    [SerializeField] private bool _isDead = false;
    [SerializeField] private bool _isAttacking = false;
    [SerializeField] private bool _isAttackReady = true;
    [SerializeField] private float _attackCooldown = 1.5f;
    [SerializeField] private List<string> _validHitTargets;

    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileContainer;

    private AnxietyDisplayController _anxietyDisplayRef;

    [Header("Events")]
    public UnityEvent OnAttack;
    public UnityEvent OnHurt;
    public UnityEvent OnDeath;



    //Monos
    private void Awake()
    {
        _moveRef = GetComponent<MoveObject>();
    }

    private void Start()
    {
        _anxietyDisplayRef = UiManager.Instance.GetAnxietyDisplayController();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }

    private void Update()
    {
        TestAttack();
        LookAtTarget();
        UpdateAttackingState();
        FleeFromTargetIfTooClose();
    }

    //Interfaces
    public void TakeDamage(int value)
    {
        _anxietyDisplayRef.DecreaseAnxiety();
        if (_anxietyDisplayRef.GetCount() == 0)
        {
            _isDead = true;
            _animController.TriggerDeathAnim();
            Destroy(gameObject, _animController.GetAnimClipLength("death_anim"));
            OnDeath?.Invoke();
        }
    }

    public void TakeDamageAndKnockBack(int value, Transform damageOrigin)
    {
        GetComponent<Rigidbody2D>().AddForce((transform.position - damageOrigin.position).normalized * Time.deltaTime * 500, ForceMode2D.Impulse);
        TakeDamage(1);
    }

    //Utils
    private void FleeFromTargetIfTooClose()
    {
        if (_target != null && _isAttacking == false && !_isDead)
        {
            float currentTargetDistance = Mathf.Abs(Vector2.Distance(transform.position, _target.transform.position));
            //Debug.Log(currentTargetDistance);
            if (currentTargetDistance <= _fleeRadius)
            {
                //SetMovement to away from target
                _moveRef.SetMoveDirection((transform.position - _target.transform.position).normalized);
            }
            else MoveToGuardPosition();
        }
    }

    private void MoveToGuardPosition()
    {
        if (_guardPosition != null)
        {
            Vector3 guardDirection = (_guardPosition.transform.position - transform.position).normalized;
            _moveRef.SetMoveDirection(guardDirection);
        }       
    }

    private void LookAtTarget()
    {
        if (_target!= null && !_isDead)
        {
            if (transform.position.x - _target.transform.position.x < 0 && _spriteRendererRef.gameObject.transform.localScale.x > 0)
                _spriteRendererRef.gameObject.transform.localScale = new Vector3(-1 * _spriteRendererRef.gameObject.transform.localScale.x, _spriteRendererRef.gameObject.transform.localScale.y, _spriteRendererRef.gameObject.transform.localScale.z);

            else if (transform.position.x - _target.transform.position.x > 0 && _spriteRendererRef.gameObject.transform.localScale.x < 0)
                _spriteRendererRef.gameObject.transform.localScale = new Vector3(Mathf.Abs(_spriteRendererRef.gameObject.transform.localScale.x), _spriteRendererRef.gameObject.transform.localScale.y, _spriteRendererRef.gameObject.transform.localScale.z);

        }
    }

    private void UpdateAttackingState()
    {
        if (_isAttacking)
        {
            DamageTargetsInAttackRange();

            //Check if the attack state ended yet
            _isAttacking = _animController.GetAttackState();
            
        }
    }


    public bool IsAttacking()
    {
        return _isAttacking;
    }

    private void Attack()
    {
        if (_isAttacking==false && _isAttackReady && !_isDead)
        {
            _animController.PlayAttackAnim();
            _isAttacking = true;
            _isAttackReady = false;
            Invoke("ReadyAttack", _attackCooldown);
            _moveRef.SetMoveDirection(Vector3.zero);
        }
    }

    private void DamageTargetsInAttackRange()
    {
        Collider2D[] detectedColliders = Physics2D.OverlapCircleAll(transform.position, _attackRadius);

        foreach (Collider2D collider in detectedColliders)
        {
            if (_validHitTargets.Contains(collider.tag))
            {
                IDamagable otherDamageHandler = collider.GetComponent<IDamagable>();
                if (otherDamageHandler != null)
                    otherDamageHandler.TakeDamageAndKnockBack(1, transform);
            }
        }
    }

    private void ReadyAttack()
    {
        _isAttackReady = true;
    }

    private void SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity, _projectileContainer);
        Vector3 directionTowardsTarget = (_target.transform.position - transform.position).normalized;
        //Debug.Log(directionTowardsTarget);
        newProjectile.GetComponent<MoveObject>().SetMoveDirection(directionTowardsTarget);
    }

    //Debugging
    private void TestAnimator()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            _animController.TriggerHurtAnim();

        if (Input.GetKeyDown(KeyCode.X))
            _animController.PlayAttackAnim();

        if (Input.GetKeyDown(KeyCode.C))
            _animController.TriggerDeathAnim();
    }

    private void TestAttack()
    {
        if (Input.GetKeyDown(KeyCode.X))
            SpawnProjectile();
    }


}
