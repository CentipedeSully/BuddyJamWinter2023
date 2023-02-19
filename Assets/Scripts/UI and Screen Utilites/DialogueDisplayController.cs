using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogueDisplayController : MonoBehaviour
{
    //Declarations
    [SerializeField] private GameObject _dialogueDisplayObject;
    [SerializeField] private TextMeshProUGUI _currentDialogueText;
    [SerializeField] private bool _isDialogueInProgress = false;

    //[SerializeField] private List<string> _nonCancelableDialogue;
    //[SerializeField] private List<string> _cancelableDialogue;

    [Header("Events")]
    public UnityEvent OnDialogueStarted;
    public UnityEvent OnDialogueContinued;
    public UnityEvent OnDialogueEnded;

    //Monobehaviors



    //Utilities
    private void ShowDialoguer()
    {
        _dialogueDisplayObject.SetActive(true);
    }

    private void HideDialoguer()
    {
        _dialogueDisplayObject.SetActive(false);
    }

    private void SetCurrentDialogueText(string newText)
    {
        _currentDialogueText.text = newText;
    }

    private IEnumerator ControlDialogue(List<string> dialogueList, bool isDialogueCancelable = true)
    {
        OnDialogueStarted?.Invoke();
        PlayerObjectManager.Instance.DisablePlayerControls();
        ShowDialoguer();

        foreach (string textItem in dialogueList)
        {
            SetCurrentDialogueText(textItem);
            bool isContinueInputPressed = false;
            bool isCancelInputPressed = false;

            while (isContinueInputPressed == false && isCancelInputPressed == false)
            {
                isContinueInputPressed = Input.GetKeyDown(KeyCode.Return);
                isCancelInputPressed = Input.GetKeyDown(KeyCode.Backspace);
                yield return null;
            }

            if (isCancelInputPressed && isDialogueCancelable)
                break;

            else OnDialogueContinued?.Invoke();
            
        }

        HideDialoguer();
        ResetDialogue();

        OnDialogueEnded?.Invoke();
        PlayerObjectManager.Instance.EnablePlayerControls();
    }

    private void ResetDialogue()
    {
        SetCurrentDialogueText("");
        _isDialogueInProgress = false;
    }

    public void EnterDialogue(List<string> dialogueList, bool isDialogueCancelable = true)
    {
        if (!_isDialogueInProgress)
        {
            _isDialogueInProgress = true;
            StartCoroutine(ControlDialogue(dialogueList, isDialogueCancelable));
        }
    }

    //public void DialogueTester()
    //{
    //    //Enter a cancelable dialgoue sequence
    //    if (Input.GetKeyDown(KeyCode.Q))
    //        EnterDialogue( _cancelableDialogue, true);

    //    //Enter a non-cancelable dialgoue sequence
    //    if (Input.GetKeyDown(KeyCode.E))
    //        EnterDialogue(_nonCancelableDialogue, false);
    //}

}
