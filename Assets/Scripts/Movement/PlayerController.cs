using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isControlsEnabled = true;
    [SerializeField] private Vector2 _moveInput;
    [SerializeField] private bool _interactionInput = false;
    [SerializeField] private bool _attackInput = false;
    private MoveObject _moveObjectReference;


    //Monobehaviors
    private void Awake()
    {
        _moveObjectReference = GetComponent<MoveObject>();   
    }

    private void Update()
    {
        ReadMoveInput();
        ReadAttackInput();
        ReadInteractionInput();

        MovePlayerBasedOnInput();
    }


    //Utilites
    private void ReadMoveInput()
    {
        _moveInput.x = Input.GetAxis("Horizontal");
        _moveInput.y = Input.GetAxis("Vertical");
    }

    private void ReadAttackInput()
    {
        _attackInput = Input.GetKeyDown(KeyCode.Space);
    }

    private void ReadInteractionInput()
    {
        _interactionInput = Input.GetKeyDown(KeyCode.Return);
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
