using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class NPCDialogue : MonoBehaviour, IInteractable
{
    //Declarations
    [SerializeField] private bool _isDialogueInteractable = true;
    [SerializeField] private bool _isDialogueSkippable = true;
    [SerializeField] private bool _isAllDialogueExhausted = false;
    [SerializeField] private int _currentDialogueIndex = 0;

    //Events
    [Header("Events")]
    public UnityEvent<NPCDialogue> OnDialogueTriggered;
    public UnityEvent OnAllDialogueExhausted;

    //monobehaviors
    


    //Interface Methods
    public void Interact()
    {
        if (_isDialogueInteractable)
            StartDialogue();
    }

    public void ForceInteract()
    {
        StartDialogue();

    }


    //Utilites
    private void StartDialogue()
    {
        OnDialogueTriggered?.Invoke(this);
        _currentDialogueIndex++;
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

    public int GetCurrentDialogueIndex()
    {
        return _currentDialogueIndex;
    }


    public void LogEventTrigger()
    {
        Debug.Log("All Dialogue Exhausted!");
    }
}

[System.Serializable]
public class ListWrapper
{
    public List<string> list;
}
