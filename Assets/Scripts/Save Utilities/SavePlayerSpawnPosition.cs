using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SavePlayerSpawnPosition : MonoBehaviour, IInteractable
{
    //Declarations
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Transform _cameraFollowObjectOnSpawn;
    [SerializeField] private bool _isPlayerFollowObjectOnSpawn = false;

    [Header("Events")]
    public UnityEvent OnSavePointInteracted;

    //Monobehaviors



    //interface utils
    public void Interact()
    {
        if (_spawnPosition != null)
        {
            PlayerObjectManager.Instance.SetCurrentPlayerSpawnPoint(_spawnPosition);

            if (_isPlayerFollowObjectOnSpawn || _cameraFollowObjectOnSpawn == null)
                PlayerObjectManager.Instance.SetIsPlayerFollowObjectAfterSpawn(true);
            else
            {
                PlayerObjectManager.Instance.SetIsPlayerFollowObjectAfterSpawn(false);
                PlayerObjectManager.Instance.SetFollowObjectAfterSpawn(_cameraFollowObjectOnSpawn);
            }

            OnSavePointInteracted?.Invoke();
        }
    }


    //Utilites




}
