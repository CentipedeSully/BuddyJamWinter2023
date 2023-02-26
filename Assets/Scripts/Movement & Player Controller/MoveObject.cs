using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    //Declarations
    private Vector3 _moveDirection;
    [SerializeField] private float _moveSpeed;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    //Monobehaviors
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveObjectViaRigidbody();
        if (_moveDirection.x != 0)
            FlipSprite();
    }

    //Utilites
    private void FlipSprite()
    {
        if (_spriteRenderer != null)
        {
            if (_moveDirection.x < -0.1 && _spriteRenderer.gameObject.transform.localScale.x > 0)
                _spriteRenderer.gameObject.transform.localScale = new Vector3(-1 * _spriteRenderer.gameObject.transform.localScale.x, _spriteRenderer.gameObject.transform.localScale.y, _spriteRenderer.gameObject.transform.localScale.z);

            else if (_moveDirection.x > 0.1 && _spriteRenderer.gameObject.transform.localScale.x < 0)
                _spriteRenderer.gameObject.transform.localScale = new Vector3(Mathf.Abs(_spriteRenderer.gameObject.transform.localScale.x), _spriteRenderer.gameObject.transform.localScale.y, _spriteRenderer.gameObject.transform.localScale.z);
        }
    }

    private void MoveObjectViaRigidbody()
    {
        if (_rigidbody2D != null)
            _rigidbody2D.AddForce(_moveDirection * _moveSpeed * Time.deltaTime);
    }

    //Getters & Setters
    public void SetMoveDirection(Vector3 newDirection)
    {
        _moveDirection = newDirection;
    }

    public Vector3 GetDirection()
    {
        return _moveDirection;
    }

    public void SetMoveSpeed(float newSpeed)
    {
        _moveSpeed = newSpeed;
    }

    public float GetSpeed()
    {
        return _moveSpeed;
    }

}
