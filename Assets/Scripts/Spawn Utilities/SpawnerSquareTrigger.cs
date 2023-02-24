using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerSquareTrigger : MonoBehaviour
{
    //Declarations
    [SerializeField] private Vector2 _sizeVector;
    [SerializeField] private SpawnController _targetController;
    [SerializeField] private List<string> _validTags;




    //Monobehaviors
    private void Update()
    {
        CheckBoundsForValidTargets();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, _sizeVector);
    }

    //Utilites
    private void CheckBoundsForValidTargets()
    {
        if (_targetController.IsEncounterActive() == false && _targetController.IsEncounterDefeated() == false)
        {
            Collider2D[] detectedColliders = Physics2D.OverlapBoxAll(transform.position, _sizeVector, 0);
            foreach (Collider2D detectedCollider in detectedColliders)
            {
                if (_validTags.Contains(detectedCollider.tag))
                {
                    _targetController.BeginEncounter();
                    break;
                }
            }
        }
        
    }



}
