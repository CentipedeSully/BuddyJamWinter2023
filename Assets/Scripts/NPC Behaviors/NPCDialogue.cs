using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class NPCDialogue : MonoBehaviour, IInteractable
{
    //Declarations
    [SerializeField] private bool _isDialogueInteractable = false;
    [SerializeField] private bool _isDialogueSkippable = true;
    [SerializeField] private List<string> dialogueList;

    [Header("Events")]
    public UnityEvent OnNpcDialogueTriggered;


    //monobehaviors
    


    //Interface Methods
    public void Interact()
    {
        StartDialogue();
    }


    //Utilites
    private void StartDialogue()
    {
        if (_isDialogueInteractable)
        {
            OnNpcDialogueTriggered?.Invoke();
            UiManager.Instance.GetDialogueControllerRef().EnterDialogue(dialogueList, _isDialogueSkippable);
        }
    }

    //Getters and Setters
    public void EnableDialogue()
    {
        _isDialogueInteractable = true;
    }

    public void DisableDialogue()
    {
        _isDialogueInteractable = false;
    }

    public bool IsDialogueInteractable()
    {
        return _isDialogueInteractable;
    }



}
