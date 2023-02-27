using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckFriendsQuester : MonoBehaviour
{
    //Declarations
    [SerializeField] bool _isRowanChecked = false;
    [SerializeField] bool _isBonnieChecked = false;
    [SerializeField] bool _isSkyChecked = false;
    [SerializeField] bool _isTeganChecked = false;

    [Header("Events")]
    public UnityEvent OnEveryoneChecked;


    //monobheaviors





    //Utilites
    public void CheckRowan()
    {
        _isRowanChecked = true;
        if (IsEveryoneChecked())
            OnEveryoneChecked?.Invoke();

    }

    public void CheckBonnie()
    {
        _isBonnieChecked = true;
        if (IsEveryoneChecked())
            OnEveryoneChecked?.Invoke();
    }
    public void CheckSky()
    {
        _isSkyChecked = true;
        if (IsEveryoneChecked())
            OnEveryoneChecked?.Invoke();
    }

    public void CheckTegan()
    {
        _isTeganChecked = true;
        if (IsEveryoneChecked())
            OnEveryoneChecked?.Invoke();
    }

    private bool IsEveryoneChecked()
    {
        if (_isTeganChecked && _isBonnieChecked && _isRowanChecked && _isSkyChecked)
            return true;
        else return false;
    }

    public bool IsQuestCompleted()
    {
        return IsEveryoneChecked();
    }


}
