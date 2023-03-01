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

    [SerializeField] private bool _isInvulnerable = false;
    [SerializeField] private bool _isDead = false;
    [SerializeField] private bool _isAttacking = false;
    [SerializeField] private bool _isAttackReady = true;
    [SerializeField] private float _attackCooldown = 1.5f;
    [SerializeField] private List<string> _validHitTargets;
    [SerializeField] private float _castCooldown = 6f;
    [SerializeField] private bool _isCastReady = true;

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
        _guardPosition = transform.parent.gameObject;
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
        TargetPlayerWheninRange();
        if (_target != null)
            CastProjectileIfItCan();
        if (!_isDead)
            AttackObjectsIfInRange();
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
            transform.parent.GetComponent<SpawnController>().ReportEnemyDeath();
            Destroy(gameObject, _animController.GetAnimClipLength("death_anim"));
            OnDeath?.Invoke();
        }
        else
        {
            if (_isAttacking)
                _animController.InterruptAttackAnim();
            _animController.TriggerHurtAnim();

            _isInvulnerable = true;
            Invoke("CooldownInvulnerability", .5f);
            _attackCooldown -= .2f;
            _castCooldown -= .2f;
        }
    }

    public void TakeDamageAndKnockBack(int value, Transform damageOrigin)
    {
        GetComponent<Rigidbody2D>().AddForce((transform.position - damageOrigin.position).normalized * Time.deltaTime * 500, ForceMode2D.Impulse);
        TakeDamage(1);
    }

    //Utils
    private void CooldownInvulnerability()
    {
        _isInvulnerable = false;
    }

    private void AttackObjectsIfInRange()
    {
        Collider2D[] detectedColliders = Physics2D.OverlapCircleAll(transform.position, _attackRadius);
        foreach (Collider2D collider in detectedColliders)
        {
            if (_validHitTargets.Contains(collider.tag))
            {
                Attack();
                break;
            }
        }
    }

    private void CastProjectileIfItCan()
    {
        if (_isAttackReady && !_isAttacking && _isCastReady && !_isDead)
        {
            SpawnProjectile();
            _animController.PlayAttackAnim();
            _isAttacking = true;
            _isAttackReady = false;
            Invoke("ReadyAttack", _attackCooldown);
            Invoke("ReadyCast", _castCooldown);
            _moveRef.SetMoveDirection(Vector3.zero);
        }    
    }

    private void TargetPlayerWheninRange()
    {
        if (_target == null)
        {
            if (Vector2.Distance(PlayerObjectManager.Instance.GetCurrentPlayerObject().transform.position, transform.position) <= 11)
                _target = PlayerObjectManager.Instance.GetCurrentPlayerObject();
        }
    }

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
        if (_guardPosition != null && !_isDead)
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
        if (_isAttacking && !_isDead)
        {
            DamageTargetsInAttackRange();

            //Check if the attack state ended yet
            _isAttacking = _animController.GetAttackState();
        }
    }
    public void SetProjectileContainer(Transform container)
    {
        _projectileContainer = container;
    }

    public bool IsAttacking()
    {
        return _isAttacking;
    }

    private void Attack()
    {
        if (_isAttacking==false && _isAttackReady && !_isDead && _target != null)
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

    private void ReadyCast()
    {
        _isCastReady = true;
    }

    private void SpawnProjectile()
    {
        if (_target != null && !_isDead)
        {
            _animController.PlayAttackAnim();
            GameObject newProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            newProjectile.transform.parent = transform.parent;
            Vector3 directionTowardsTarget = (_target.transform.position - transform.position).normalized;
            //Debug.Log(directionTowardsTarget);
            newProjectile.GetComponent<MoveObject>().SetMoveDirection(directionTowardsTarget);
        }

    }

    public void TargetPlayer()
    {
        _target = PlayerObjectManager.Instance.GetCurrentPlayerObject();
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
