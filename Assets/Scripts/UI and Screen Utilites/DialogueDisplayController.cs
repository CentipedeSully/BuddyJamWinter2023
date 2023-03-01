using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogueDisplayController : MonoBehaviour
{
    //Declarations
    [SerializeField] private float _minimumTextDisplayTime = .3f;
    private WaitForSeconds _minDisplayTimeWFS;

    [SerializeField] private bool _isDialogueInProgress = false;

    [SerializeField] private List<GameObject> _oliveEnterDialogue;
    [SerializeField] private List<GameObject> _oliveExitDialogue;
    [SerializeField] private List<GameObject> _computerDialogue;
    [SerializeField] private List<GameObject> _lampDialogue;
    [SerializeField] private List<GameObject> _dresserDialogue;
    [SerializeField] private List<GameObject> _bedDialogue;
    [SerializeField] private List<GameObject> _bonnieDialogue;
    [SerializeField] private List<GameObject> _rowanDialogue;
    [SerializeField] private List<GameObject> _skylarDialogue;
    [SerializeField] private List<GameObject> _teganDialogue;
    [SerializeField] private List<GameObject> _combatRoomEnterDialogue;
    [SerializeField] private List<GameObject> _combatRoomExitDialogue;
    [SerializeField] private List<GameObject> _bossRoomDialogue;
    [SerializeField] private List<GameObject> _finalRoomDialogue;
    [SerializeField] private List<GameObject> _closeDialogue;


    [Header("Events")]
    public UnityEvent OnDialogueStarted;
    public UnityEvent OnDialogueContinued;
    public UnityEvent OnDialogueEnded;



    //Monobehaviors
    private void Awake()
    {
        _minDisplayTimeWFS = new WaitForSeconds(_minimumTextDisplayTime);
    }


    //Utilities
    private IEnumerator StepThroughDialogue(List<GameObject> dialogueList, NPCDialogue originScript, bool adjustPlayerControls = true)
    {
        _isDialogueInProgress = true;

        if (adjustPlayerControls)
            PlayerObjectManager.Instance.DisablePlayerControls();
        OnDialogueStarted?.Invoke();
        int count = 0;
        while (count < dialogueList.Count)
        {
            //show dialogue
            dialogueList[count].SetActive(true);

            //wait for the minimum display time
            yield return _minDisplayTimeWFS;

            //wait for input
            while (Input.GetKeyDown(KeyCode.Space) == false)
                yield return null;

            //close current dialogue and invoke continued event after input
            dialogueList[count].SetActive(false);
            OnDialogueContinued?.Invoke();
            count++;
        }

        if (adjustPlayerControls)
            PlayerObjectManager.Instance.EnablePlayerControls();
        _isDialogueInProgress = false;
        originScript.OnAllDialogueExhausted?.Invoke();
    }

    public bool IsDialogueinProgress()
    {
        return _isDialogueInProgress;
    }

    public void ShowFirstRoomStarttDialogue(NPCDialogue originSript)
    {
        if (!_isDialogueInProgress)
            StartCoroutine(StepThroughDialogue(_oliveEnterDialogue, originSript));
    }

    public void ShowFirstRoomLeaveDialogue(NPCDialogue originSript)
    {
        if (!_isDialogueInProgress)
            StartCoroutine(StepThroughDialogue(_oliveExitDialogue, originSript));
    }

    public void ShowLampDialogue(NPCDialogue originSript)
    {
        if (!_isDialogueInProgress)
            StartCoroutine(StepThroughDialogue(_lampDialogue, originSript));
    }

    public void ShowComputerDialogue(NPCDialogue originSript)
    {
        if (!_isDialogueInProgress)
            StartCoroutine(StepThroughDialogue(_computerDialogue, originSript));
    }

    public void ShowDresserDialogue(NPCDialogue originSript)
    {
        if (!_isDialogueInProgress)
            StartCoroutine(StepThroughDialogue(_dresserDialogue, originSript));
    }

    public void ShowBedDialogue(NPCDialogue originSript)
    {
        if (!_isDialogueInProgress)
            StartCoroutine(StepThroughDialogue(_bedDialogue, originSript));
    }

    public void ShowBonnieDialogue(NPCDialogue originSript)
    {
        if (!_isDialogueInProgress)
            StartCoroutine(StepThroughDialogue(_bonnieDialogue, originSript));
    }

    public void ShowTeganDialogue(NPCDialogue originSript)
    {
        if (!_isDialogueInProgress)
            StartCoroutine(StepThroughDialogue(_teganDialogue, originSript));
    }

    public void ShowRowanDialogue(NPCDialogue originSript)
    {
        if (!_isDialogueInProgress)
            StartCoroutine(StepThroughDialogue(_rowanDialogue, originSript));
    }

    public void ShowSkylarDialogue(NPCDialogue originSript)
    {
        if (!_isDialogueInProgress)
            StartCoroutine(StepThroughDialogue(_skylarDialogue, originSript));
    }

    public void ShowCombatRoomEnterDialogue(NPCDialogue originSript)
    {
        if (!_isDialogueInProgress)
            StartCoroutine(StepThroughDialogue(_combatRoomEnterDialogue, originSript));
    }

    public void ShowCombatRoomExitDialogue(NPCDialogue originSript)
    {
        if (!_isDialogueInProgress)
            StartCoroutine(StepThroughDialogue(_combatRoomExitDialogue, originSript));
    }

    public void ShowBossDialogue(NPCDialogue originSript)
    {
        if (!_isDialogueInProgress)
            StartCoroutine(StepThroughDialogue(_bossRoomDialogue, originSript));
    }


    public void ShowFinalRoomDialogue(NPCDialogue originSript)
    {
        if (!_isDialogueInProgress)
            StartCoroutine(StepThroughDialogue(_finalRoomDialogue, originSript));
    }

    public void ShowCloseDialogue(NPCDialogue originSript)
    {
        if (!_isDialogueInProgress)
            StartCoroutine(StepThroughDialogue(_closeDialogue, originSript, false));
    }


    /*
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
                isContinueInputPressed = Input.GetKeyDown(KeyCode.Space);
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
    */

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
