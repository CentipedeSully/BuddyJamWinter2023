using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxietyAnimController : MonoBehaviour
{
    //declarations
    [SerializeField] private Animator _animatorRef;
    [SerializeField] private string _attackingBoolName = "isAttacking";
    [SerializeField] private string _hurtTriggerName = "onHurt";
    [SerializeField] private string _deathtriggerName = "onDeath";

    [SerializeField] private string _attackClipName = "attack_anim";
    [SerializeField] private string _hurtClipName = "hurt_anim";
    [SerializeField] private string _deathClipName = "death_anim";
    [SerializeField] private string _idleClipName = "idle_anim";

    private WaitForSeconds _attackDurationWFS;

    //monos
    private void Awake()
    {
        if (_animatorRef == null)
            _animatorRef = GetComponent<Animator>();
        if (_animatorRef != null)
            _attackDurationWFS = new WaitForSeconds(GetAnimClipLength(_attackClipName));
    }



    //Utils
    public void TriggerHurtAnim()
    {
        _animatorRef.SetTrigger(_hurtTriggerName);
    }

    public void TriggerDeathAnim()
    {
        _animatorRef.SetTrigger(_deathtriggerName);
    }

    public void PlayAttackAnim()
    {
        if (GetAttackState() == false)
        {
            _animatorRef.SetBool(_attackingBoolName, true);
            StartCoroutine(CountAttackDuration());
        }
    }

    public void InterruptAttackAnim()
    {
        if (GetAttackState() == true)
        {
            _animatorRef.SetBool(_attackingBoolName, false);
            StopAllCoroutines();
        }
    }

    private IEnumerator CountAttackDuration()
    {
        yield return _attackDurationWFS;
        if (GetAttackState() == true)
            _animatorRef.SetBool(_attackingBoolName, false);
    }

    //getters & Setters
    public bool GetAttackState()
    {
        return _animatorRef.GetBool(_attackingBoolName);
    }

    public float GetAnimClipLength(string animClipName)
    {
        if (_animatorRef != null)
        {
            AnimationClip[] animClips = _animatorRef.runtimeAnimatorController.animationClips;

            foreach (AnimationClip clip in animClips)
            {
                if (clip.name == animClipName)
                    return clip.length;
            }
            return 0;
        }
        else return 0;
    }

}
