using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SquareTrigger : MonoBehaviour
{
    //Declarations
    [SerializeField] private Vector2 _sizeVector;
    [SerializeField] private bool _isTriggerActive = true;
    [SerializeField] private List<string> _validTags;

    [Header("Events")]
    public UnityEvent OnTriggered;


    //Monobehaviors
    private void Update()
    {
        CheckBoundsForValidTargetsIfActive();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, _sizeVector);
    }


    //Utilites
    private void CheckBoundsForValidTargetsIfActive()
    {
        if (_isTriggerActive)
        {
            Collider2D[] detectedColliders = Physics2D.OverlapBoxAll(transform.position, _sizeVector, 0);
            foreach (Collider2D detectedCollider in detectedColliders)
            {
                //Debug.Log(detectedCollider.tag);
                if (_validTags.Contains(detectedCollider.tag))
                {
                    OnTriggered?.Invoke();
                    _isTriggerActive = false;
                    break;
                }
            }
        }
    }

    public void ActivateTrigger()
    {
        _isTriggerActive = true;
    }

    public void DeactivateTrigger()
    {
        _isTriggerActive = false;
    }

}
