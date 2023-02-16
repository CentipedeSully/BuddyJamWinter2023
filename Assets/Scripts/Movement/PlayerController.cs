using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isControlsEnabled = true;
    [SerializeField] private Vector2 _moveInput;
    private MoveObject _moveObjectReference;


    //Monobehaviors
    private void Awake()
    {
        _moveObjectReference = GetComponent<MoveObject>();   
    }

    private void Update()
    {
        ReadMoveInput();
        MovePlayerBasedOnInput();
    }


    //Utilites
    private void ReadMoveInput()
    {
        _moveInput.x = Input.GetAxis("Horizontal");
        _moveInput.y = Input.GetAxis("Vertical");
    }
    private void MovePlayerBasedOnInput()
    {
        if (_isControlsEnabled)
            _moveObjectReference.SetMoveDirection(_moveInput);
        else
            _moveObjectReference.SetMoveDirection(Vector2.zero);
    }

    public void DisableControls()
    {
        _isControlsEnabled = false;
    }

    public void EnableControls()
    {
        _isControlsEnabled = true;
    }
}
