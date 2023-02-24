using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathBehavior : DeathBehavior
{
    //inherits directly from DeathBehavior
    public override void EnterDeathSequence()
    {
        LocalSpawnControllerReference localSpawnControllerReference = transform.parent.GetComponent<LocalSpawnControllerReference>();
        if (localSpawnControllerReference != null)
            localSpawnControllerReference.GetSpawnController().ReportEnemyDeath(); 

        base.EnterDeathSequence();
    }

}
