using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnController : MonoBehaviour
{
    //Declarations
    [SerializeField] private List<GameObject> _enemyPrefabs;
    [SerializeField] private List<Transform> _spawnPositions;
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private int _amountToSpawn = 10;
    [SerializeField] private int _amountSpawned = 0;
    [SerializeField] private int _enemiesInRoomMax = 3;
    [SerializeField] private int _enemiesDefeated = 0;
    [SerializeField] private bool _isEncounterActive = false;
    [SerializeField] private bool _isEncounterDefeated = false;

    private IEnumerator _encounterHandlerRef;

    [SerializeField] private List<string> StartingDialogue;
    [SerializeField] private List<string> EndingDialogue;

    [Header("Events")]
    public UnityEvent OnEncounterStarted;
    public UnityEvent OnEncounterEnded;
    public UnityEvent OnEncounterDefeated;


    //Monobehaviors




    //Utilities
    private IEnumerator ManageEncounter()
    {
        _isEncounterActive = true;
        OnEncounterStarted?.Invoke();

        while (_isEncounterDefeated == false && PlayerObjectManager.Instance.IsPlayerAlive())
        {
            //Spawn enemy if possible
            if (_amountSpawned < _amountToSpawn && _enemyContainer.childCount < _enemiesInRoomMax)
            {
                Transform randomGuardPosition = _spawnPositions[Random.Range(0, _spawnPositions.Count)];
                GameObject newEnemy = Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)], randomGuardPosition.position, Quaternion.identity, _enemyContainer);
                _amountSpawned++;

                if (newEnemy.GetComponent<HostileMeleeBehavior>() != null)
                    newEnemy.GetComponent<HostileMeleeBehavior>().SetGuardPosition(randomGuardPosition);
            }

            else if (_enemiesDefeated == _amountToSpawn)
            {
                _isEncounterDefeated = true;
                OnEncounterDefeated?.Invoke();
            }

            yield return null;
        }

        if (PlayerObjectManager.Instance.IsPlayerAlive() == false && _isEncounterDefeated == false)
        {
            //Reset Utilites
            _enemiesDefeated = 0;
            _amountSpawned = 0;

            //Delete the enemies in the room
            foreach (Transform child in transform)
                Destroy(child.gameObject); // Typically, destroying like this wouldn't work, but Destroy() defers the destroy unitl after the frame ends, not immediately.

        }

        OnEncounterEnded?.Invoke();
        _isEncounterActive = false;
        _encounterHandlerRef = null;
    }

    public bool IsEncounterDefeated()
    {
        return _isEncounterDefeated;
    }

    public bool IsEncounterActive()
    {
        return _isEncounterActive;
    }

    public void BeginEncounter()
    {
        if (_isEncounterActive == false && _encounterHandlerRef == null)
        {
            _encounterHandlerRef = ManageEncounter();
            StartCoroutine(_encounterHandlerRef);
        }
    }

    public void ReportEnemyDeath()
    {
        _enemiesDefeated++;
    }



}
