using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeathBehavior : MonoBehaviour, IDeathBehavior
{
    //Declarations
    [SerializeField] protected string _deathAnimClipName;
    [SerializeField] protected string _triggerParameterName;
    protected float _animDuration;
    protected Animator _animatorReference;


    //Monobehaviors
    private void Awake()
    {
        _animatorReference = GetComponent<Animator>();
        GetAnimationClipLength(_deathAnimClipName);
    }


    //Utilites
    private void GetAnimationClipLength(string animationClipName)
    {
        AnimationClip[] animationClips = _animatorReference.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in animationClips)
        {
            if (clip.name == _deathAnimClipName)
            {
                _animDuration = clip.length;
                break;
            }
        }
    }

    public virtual void EnterDeathSequence()
    {
        //Trigger anim
        _animatorReference.SetTrigger(_triggerParameterName);

        //Destroy self after anim finishes
        Destroy(gameObject, _animDuration);
    }
}
