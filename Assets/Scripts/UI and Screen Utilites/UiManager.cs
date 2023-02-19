using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class UiManager : MonoSingleton<UiManager>
{
    //Declarations
    [SerializeField] private ScreenFadeController _fadeControllerRef;
    [SerializeField] private DialogueDisplayController _dialogueControllerRef;


    //Monobehaviors



    //Utilites

    //Getters & Setters
    public ScreenFadeController GetScreenFadeController()
    {
        return _fadeControllerRef;
    }

    public DialogueDisplayController GetDialogueControllerRef()
    {
        return _dialogueControllerRef;
    }

}
