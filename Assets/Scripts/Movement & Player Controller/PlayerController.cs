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
    private PlayerInteractionBehavior _interactBehaviorReference;
    private PlayerAttackBehavior _attackBehaviorRef;
    private FMOD.Studio.EventInstance instance;


    //Monobehaviors
    private void Awake()
    {
        _moveObjectReference = GetComponent<MoveObject>();
        _interactBehaviorReference = GetComponent<PlayerInteractionBehavior>();
        _attackBehaviorRef = GetComponent<PlayerAttackBehavior>();
    }

    private void Update()
    {
        ReadMoveInput();
        ReadAttackInput();
        ReadInteractionInput();

        MovePlayerBasedOnInput();
        AttackBasedOnOnInput();
        InteractBasedOnInput();
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
        _interactionInput = Input.GetKey(KeyCode.F);
        //if (_interactionInput == true) Debug.Log("Interact Pressed");
    }

    private void MovePlayerBasedOnInput()
    {
        if (_isControlsEnabled)
            _moveObjectReference.SetMoveDirection(_moveInput);
        else
            _moveObjectReference.SetMoveDirection(Vector2.zero);

        if (_isControlsEnabled)
        {
            _moveObjectReference.SetMoveDirection(_moveInput);

            // Start/stop the footstep loop based on whether the player is moving
            if (_moveInput.magnitude > 0)
            {
                if (!instance.isValid())
                {
                    instance = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Player_Footsteps");
                    instance.start();
                }
            }
            else
            {
                if (instance.isValid())
                {
                    instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    instance.release();
                }
            }
        }
        else
        {
            _moveObjectReference.SetMoveDirection(Vector2.zero);
        }
    }

    private void InteractBasedOnInput()
    {
        if (_isControlsEnabled && _interactionInput)
            _interactBehaviorReference.InteractWithSurroundings();
    }

    private void AttackBasedOnOnInput()
    {
        if (_isControlsEnabled && _attackInput)
            _attackBehaviorRef.EnterAttack();

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
