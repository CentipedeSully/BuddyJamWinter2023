using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    //Declarations
    private Vector3 _moveDirection;
    [SerializeField] private float _moveSpeed;
    private Rigidbody2D _rigidbody2D;

    //Monobehaviors
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveObjectViaRigidbody();
    }

    //Utilites
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
