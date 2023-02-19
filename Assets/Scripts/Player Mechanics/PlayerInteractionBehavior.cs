using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionBehavior : MonoBehaviour
{
    //Declarations
    [SerializeField] private float _interactionRadius = 1;
    private Collider2D[] _colliderDetectionResults;

    //Monobehaviors
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _interactionRadius);
    }


    //Utilities
    public void InteractWithSurroundings()
    {
        _colliderDetectionResults = Physics2D.OverlapCircleAll(transform.position, _interactionRadius);
        IInteractable[] interactableComponents;

        foreach (Collider2D detectedCollider in _colliderDetectionResults)
        {
            interactableComponents = detectedCollider.GetComponents<IInteractable>();

            for (int i = 0; i < interactableComponents.Length; i++)
            {
                Debug.Log("Interactable component: " + interactableComponents[i].ToString());
                interactableComponents[i].Interact();
            }
        }
    }



}
