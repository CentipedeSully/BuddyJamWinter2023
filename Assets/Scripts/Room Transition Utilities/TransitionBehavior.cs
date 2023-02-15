using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransitionBehavior : MonoBehaviour
{
    //Declarations
    [Header("Transition Settings")]
    [SerializeField] private Transform _targetLocation;
    [SerializeField] private float _transitionDuration = 2;
    [SerializeField] private string _validObjectTag = "Player";
    [SerializeField] private bool _isLocked = false;

    [Header("Events")]
    public UnityEvent OnTransitionStarted;
    public UnityEvent OnTransitionEnded;



    //monobehaviors
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isLocked && collision.tag == _validObjectTag)
            StartCoroutine(TransitionObject(collision.gameObject));
    }



    //Utilites
    private IEnumerator TransitionObject(GameObject triggeringObject)
    {
        OnTransitionStarted?.Invoke();
        yield return new WaitForSeconds(_transitionDuration / 2);

        triggeringObject.transform.position = _targetLocation.transform.position;

        yield return new WaitForSeconds(_transitionDuration / 2);
        OnTransitionEnded?.Invoke();
    }
}
