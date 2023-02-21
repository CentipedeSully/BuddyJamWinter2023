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
    [SerializeField] private Transform _cameraFollowTransformOnRespawn;
    [SerializeField] private bool _isPlayerFollowObjectOnSpawn = false;


    //Monobehaviors



    //Utilities
    public void SpawnPlayer()
    {
        if (IsPlayerAlive() == false)
        {
            _currentPlayerObject = Instantiate(_playerPrefab, _currentPlayerSpawnPoint.position, Quaternion.identity, gameObject.transform);
            _playerControllerRef = _currentPlayerObject.GetComponent<PlayerController>();

            if (_isPlayerFollowObjectOnSpawn || _cameraFollowTransformOnRespawn == null)
                VirtualCameraHandler.Instance.SetNewFollowTarget(_currentPlayerObject.transform);
            else VirtualCameraHandler.Instance.SetNewFollowTarget(_cameraFollowTransformOnRespawn);

            GetComponent<PlayerGUIHealthDisplayController>().SetHealthBehavior(_currentPlayerObject.GetComponent<HealthBehavior>());
            GetComponent<PlayerGUIHealthDisplayController>().SetupGUIDisplay();

            DisablePlayerControls();
            UiManager.Instance.GetScreenFadeController().FadeToTransparent(2);
            Invoke("EnablePlayerControls", 2);
        }
            
    }

    public void DisablePlayerControls()
    {
        if (_playerControllerRef != null)
            _playerControllerRef.DisableControls();
    }

    public void EnablePlayerControls()
    {
        if (_playerControllerRef != null)
            _playerControllerRef.EnableControls();
    }

    public void ReportPlayerDeath()
    {
        _currentPlayerObject = null;
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

    public void SetIsPlayerFollowObjectAfterSpawn(bool value)
    {
        _isPlayerFollowObjectOnSpawn = value;
    }

    public void SetFollowObjectAfterSpawn(Transform newTransform)
    {
        _cameraFollowTransformOnRespawn = newTransform;
    }


    //Test Utils
    private void SpawnPlayerOnCommand()
    {
        if (Input.GetKeyDown(KeyCode.P))
            SpawnPlayer();
    }

}
