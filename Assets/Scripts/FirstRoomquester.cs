using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstRoomquester : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isLampInteractedeWith = false;
    [SerializeField] private bool _isBedInteractedeWith = false;
    [SerializeField] private bool _isDresserInteractedeWith = false;
    [SerializeField] private bool _isComputerInteractedeWith = false;
    [SerializeField] private bool _isRowanInteractedeWith = false;
    [SerializeField] private bool _isBonnieInteractedeWith = false;
    [SerializeField] private bool _isSkylarInteractedeWith = false;
    [SerializeField] private bool _isTeganInteractedeWith = false;

    [SerializeField] private bool _isNextRoomUnlocked = false;

    [Header("Events")]
    public UnityEvent OnEverythingInteractedWith;

    //utils
    private bool IsEverythingChecked()
    {
        if (_isLampInteractedeWith && _isBedInteractedeWith && _isDresserInteractedeWith && _isComputerInteractedeWith
            && _isRowanInteractedeWith && _isBonnieInteractedeWith && _isSkylarInteractedeWith && _isTeganInteractedeWith)
            return true;
        else return false;
    }

    public void CheckLamp()
    {
        _isLampInteractedeWith = true;

        if (IsEverythingChecked() && !_isNextRoomUnlocked)
        {
            OnEverythingInteractedWith?.Invoke();
            _isNextRoomUnlocked = true;
        }
    }

    public void CheckBed()
    {
        _isBedInteractedeWith = true;

        if (IsEverythingChecked() && !_isNextRoomUnlocked)
        {
            OnEverythingInteractedWith?.Invoke();
            _isNextRoomUnlocked = true;
        }
    }

    public void CheckDresser()
    {
        _isDresserInteractedeWith = true;

        if (IsEverythingChecked() && !_isNextRoomUnlocked)
        {
            OnEverythingInteractedWith?.Invoke();
            _isNextRoomUnlocked = true;
        }
    }

    public void CheckComputer()
    {
        _isComputerInteractedeWith = true;

        if (IsEverythingChecked() && !_isNextRoomUnlocked)
        {
            OnEverythingInteractedWith?.Invoke();
            _isNextRoomUnlocked = true;
        }
    }

    public void CheckRowan()
    {
        _isRowanInteractedeWith = true;

        if (IsEverythingChecked() && !_isNextRoomUnlocked)
        {
            OnEverythingInteractedWith?.Invoke();
            _isNextRoomUnlocked = true;
        }
    }

    public void CheckTegan()
    {
        _isTeganInteractedeWith = true;

        if (IsEverythingChecked() && !_isNextRoomUnlocked)
        {
            OnEverythingInteractedWith?.Invoke();
            _isNextRoomUnlocked = true;
        }
    }

    public void CheckBonnie()
    {
        _isBonnieInteractedeWith = true;

        if (IsEverythingChecked() && !_isNextRoomUnlocked)
        {
            OnEverythingInteractedWith?.Invoke();
            _isNextRoomUnlocked = true;
        }
    }

    public void CheckSkylar()
    {
        _isSkylarInteractedeWith = true;

        if (IsEverythingChecked() && !_isNextRoomUnlocked)
        {
            OnEverythingInteractedWith?.Invoke();
            _isNextRoomUnlocked = true;
        }
    }

}
