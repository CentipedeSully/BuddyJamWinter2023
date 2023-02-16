using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class PlayerObjectManager : MonoSingleton<PlayerObjectManager>
{
    //Declarations
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _currentPlayerObject;
    [SerializeField] private Transform _currentPlayerSpawnPoint;
    [SerializeField] private PlayerController _playerControllerRef;


    //Monobehaviors



    //Utilities
    public void SpawnPlayer()
    {
        if (_currentPlayerObject == null)
        {
            _currentPlayerObject = Instantiate(_playerPrefab, _currentPlayerSpawnPoint.position, Quaternion.identity, gameObject.transform);
            _playerControllerRef = _currentPlayerObject.GetComponent<PlayerController>();
        }
            
    }

    public void DisablePlayerControls()
    {
        _playerControllerRef.DisableControls();
    }

    public void EnablePlayerControls()
    {
        _playerControllerRef.EnableControls();
    }


    //Getters and Setters
    public GameObject GetCurrentPlayerObject()
    {
        return _currentPlayerObject;
    }

    public bool IsPlayerAlive()
    {
        return _currentPlayerObject != null;
    }

    public Transform GetCurrentplayerSpawnPoint()
    {
        return _currentPlayerSpawnPoint;
    }

    public void SetCurrentPlayerSpawnPoint(Transform newSpawnPoint)
    {
        if (newSpawnPoint != null)
            _currentPlayerSpawnPoint = newSpawnPoint;
    }


}
