using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class UiManager : MonoSingleton<UiManager>
{
    //Declarations
    [SerializeField] private ScreenFadeController _fadeControllerRef;
    [SerializeField] private DialogueDisplayController _dialogueControllerRef;
    [SerializeField] private AnxietyDisplayController _anxietyDisplayControllerRef;
    [SerializeField] private ScreenManager _screenManagerRef;

    //Monobehaviors



    //Utilites

    //Getters & Setters
    public ScreenManager GetScreenManager()
    {
        return _screenManagerRef;
    }

    public ScreenFadeController GetScreenFadeController()
    {
        return _fadeControllerRef;
    }

    public DialogueDisplayController GetDialogueControllerRef()
    {
        return _dialogueControllerRef;
    }

    public AnxietyDisplayController GetAnxietyDisplayController()
    {
        return _anxietyDisplayControllerRef;
    }

}
