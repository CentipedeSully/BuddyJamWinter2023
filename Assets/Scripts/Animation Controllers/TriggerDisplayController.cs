using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDisplayController : MonoBehaviour
{
    [SerializeField] private Animator _animatorRef;
    [SerializeField] private string _boolName = "isVisible";

    public void ShowTriggerSpace()
    {
        _animatorRef.SetBool(_boolName, true);
    }

    public void HideTriggerSpace()
    {
        _animatorRef.SetBool(_boolName, false);
    }
}
