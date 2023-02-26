using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    //Declarations
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private string _deathTriggerName = "OnDeath";
    [SerializeField] private string _moveBoolName = "isMoving";
    [SerializeField] private string _attackBoolName = "isAttacking";


    //Monobehaviors
    private void Awake()
    {
        if (_playerAnimator == null)
            _playerAnimator = GetComponent<Animator>();
    }


    //Utilites
    public void TriggerDeathAnim()
    {
        _playerAnimator?.SetTrigger(_deathTriggerName);
    }

    public void SetMoveAnimState(bool newValue)
    {
        _playerAnimator?.SetBool(_moveBoolName, newValue);
    }

    public void SetAttackAnimState(bool newValue)
    {
        _playerAnimator?.SetBool(_attackBoolName, newValue);
    }

    public float GetAnimClipLength(string animClipName)
    {
        if (_playerAnimator != null)
        {
            AnimationClip[] animClips = _playerAnimator.runtimeAnimatorController.animationClips;

            foreach (AnimationClip clip in animClips)
            {
                if (clip.name == animClipName)
                    return clip.length;
            }
            return 0;
        }
        else return 0;
    }

    public bool IsPlayerMoveAnimPlaying()
    {
        return _playerAnimator.GetBool(_moveBoolName);
    }

    public bool IsPlayerAttackAnimPlaying()
    {
        return _playerAnimator.GetBool(_attackBoolName);
    }
}
