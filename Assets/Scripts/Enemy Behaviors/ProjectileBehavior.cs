using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour , IDamagable
{
    //Declarations
    [SerializeField] private float _speedIncrement = 300;
    [SerializeField] private MoveObject _moveRef;
    [SerializeField] private string _validTag = "Player";



    //Monobehaviors
    private void Awake()
    {
        _moveRef = GetComponent<MoveObject>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == _validTag)
        {
            IDamagable targetDamageHandler = collision.collider.GetComponent<IDamagable>();
            if (targetDamageHandler != null)
                targetDamageHandler.TakeDamageAndKnockBack(1, transform);
        }

        if (collision.collider.tag != "Enemy")
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == _validTag)
        {
            if (collision.tag == "Player")
            {
                if (collision.GetComponent<PlayerAttackBehavior>().IsAttacking())
                    RedirectProjectile();
                else
                {
                    IDamagable targetDamageHandler = collision.GetComponent<IDamagable>();
                    if (targetDamageHandler != null)
                        targetDamageHandler.TakeDamageAndKnockBack(1, transform);
                }
                Destroy(gameObject);
            }

            else if (collision.tag == "Enemy")
            {
                if (collision.GetComponent<AnxietyBehavior>().IsAttacking())
                    RedirectProjectile();

                else
                {
                    IDamagable targetDamageHandler = collision.GetComponent<IDamagable>();
                    if (targetDamageHandler != null)
                        targetDamageHandler.TakeDamageAndKnockBack(1, transform);
                }
                Destroy(gameObject);
            }
        }

        else if (collision.tag != "Enemy" && collision.tag!= "Player")
            Destroy(gameObject);
    }

    //Interface
    public void TakeDamage(int value)
    {
        RedirectProjectile();
    }

    public void TakeDamageAndKnockBack(int value, Transform entityPosition)
    {
        RedirectProjectile();
    }


    //Utilities
    private void RedirectProjectile()
    {
        if (_validTag == "Player")
        {
            _validTag = "Enemy";
            tag = "PlayerProjectile";
        }

        else
        {
            _validTag = "Player";
            tag = "Enemy";
        }
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _moveRef.SetMoveDirection(-_moveRef.GetDirection());
        
        SpeedUpProjectile();
    }

    private void SpeedUpProjectile()
    {
        _moveRef.SetMoveSpeed(_moveRef.GetSpeed() + _speedIncrement);
    }
}
