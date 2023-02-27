using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    //Declarations
    [SerializeField] private string _deathTriggerName;
    [SerializeField] private Animator _animReference;


    //Monobehaviors
    private void Awake()
    {
        if (_animReference == null)
            _animReference = GetComponent<Animator>();
    }


    //Utilites
    public void TriggerDeath()
    {
        _animReference.SetTrigger(_deathTriggerName);
    }





}
