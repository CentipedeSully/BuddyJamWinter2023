using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathBehavior : DeathBehavior
{
    //Declarations
    [SerializeField] private float _deathFadeoutDuration = 5;
    private FMOD.Studio.EventInstance instance;

    //Monos



    //Utils
    private void TriggerRespawnAndDestroyPlayer()
    {
        PlayerObjectManager.Instance.SpawnPlayer();
        Destroy(gameObject);
        //End Death SFX Loop
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();

    }

    public override void EnterDeathSequence()
    {
        //Disable controls
        PlayerObjectManager.Instance.DisablePlayerControls();
        PlayerObjectManager.Instance.ReportPlayerDeath();

        //Trigger player sleep anim
        GetComponent<PlayerAnimController>()?.TriggerDeathAnim();

        //Start Death SFX Loop
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Player_Death");
        instance.start();

        //FadeTo Black
        UiManager.Instance.GetScreenFadeController().FadeToBlack(_deathFadeoutDuration);
        Invoke("TriggerRespawnAndDestroyPlayer", _deathFadeoutDuration);
        
    }
}
