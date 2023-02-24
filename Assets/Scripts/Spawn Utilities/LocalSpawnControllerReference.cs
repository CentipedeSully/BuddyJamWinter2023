using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSpawnControllerReference : MonoBehaviour
{
    [SerializeField] private SpawnController _spawnControllerRef;

    public SpawnController GetSpawnController()
    {
        return _spawnControllerRef;
    }
}
