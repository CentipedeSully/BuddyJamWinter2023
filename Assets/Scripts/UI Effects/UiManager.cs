using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class UiManager : MonoSingleton<UiManager>
{
    //Declarations
    [SerializeField] private ScreenFadeController _fadeControllerRef;


    //Monobehaviors



    //Utilites

    //Getters & Setters
    public ScreenFadeController GetScreenFadeController()
    {
        return _fadeControllerRef;
    }



}
