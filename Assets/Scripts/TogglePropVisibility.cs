using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePropVisibility : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isHidden = true;
    [SerializeField] private Animator _animatorRef;
    [SerializeField] private string _boolParamName = "isHidden";


    //Monobehaviors



    //Utilites
    public void FadeObject()
    {
        if (_animatorRef != null)
            _animatorRef.SetBool(_boolParamName, true);
        _isHidden = _animatorRef.GetBool(_boolParamName);
    }

    public void ShowObject()
    {
        if (_animatorRef != null)
            _animatorRef.SetBool(_boolParamName, false);
        _isHidden = _animatorRef.GetBool(_boolParamName);
    }

    public bool IsObjectHidden()
    {
        return _isHidden;
    }

}
