using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SavePlayerSpawnPosition : MonoBehaviour, IInteractable
{
    //Declarations
    [SerializeField] private Transform _spawnPosition;

    [Header("Events")]
    public UnityEvent OnSavePointInteracted;

    //Monobehaviors



    //interface utils
    public void Interact()
    {
        if (_spawnPosition != null)
        {
            PlayerObjectManager.Instance.SetCurrentPlayerSpawnPoint(_spawnPosition);
            OnSavePointInteracted?.Invoke();
        }
    }


    //Utilites




}
