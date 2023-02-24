using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathBehavior : DeathBehavior
{
    //Declarations
    [SerializeField] private float _deathFadeoutDuration = 5;

    //Monos



    //Utils
    private void TriggerRespawnAndDestroyPlayer()
    {
        PlayerObjectManager.Instance.SpawnPlayer();
        Destroy(gameObject);
    }

    public override void EnterDeathSequence()
    {
        //Disable controls
        PlayerObjectManager.Instance.DisablePlayerControls();
        PlayerObjectManager.Instance.ReportPlayerDeath();

        //Trigger player sleep anim
        _animatorReference.SetTrigger(_triggerParameterName);

        //FadeTo Black
        UiManager.Instance.GetScreenFadeController().FadeToBlack(_deathFadeoutDuration);
        Invoke("TriggerRespawnAndDestroyPlayer", _deathFadeoutDuration);
        
    }
}
