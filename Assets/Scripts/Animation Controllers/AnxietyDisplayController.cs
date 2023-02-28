using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxietyDisplayController : MonoBehaviour
{
    //Declarations
    [SerializeField] private List<Animator> _anxietyAnimators;
    [SerializeField] private Animator _animatorRef;
    [SerializeField] private string _boolParamName = "isVisible";
    [SerializeField] private int _count = 0;


    //Monobehaviors
    private void Awake()
    {
        _animatorRef = GetComponent<Animator>();
    }


    //Utilites
    private bool GetAnimState(Animator animatorRef)
    {
        return animatorRef.GetBool(_boolParamName);
    }
    private void SetAnimState(Animator animatorRef, bool value)
    {
        animatorRef.SetBool(_boolParamName, value);
    }

    public void IncreaseAnxiety()
    {
        foreach (Animator animRef in _anxietyAnimators)
        {
            if (GetAnimState(animRef) == false)
            {
                SetAnimState(animRef, true);
                _count++;
                break;
            }

            if (_count == 15)
            {
                //Stop Boss Encounter
                //Show Defeat screen
            }
        }
    }

    public void DecreaseAnxiety()
    {
        for (int i = _anxietyAnimators.Count - 1; i > -1; i--)
        {
            if (GetAnimState(_anxietyAnimators[i]) == true)
            {
                SetAnimState(_anxietyAnimators[i], false);
                _count--;
                break;
            }
        }
    }

    public int GetCount()
    {
        return _count;
    }

}
