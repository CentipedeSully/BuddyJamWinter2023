using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    //Declarations
    [SerializeField] private GameObject _titleScreen;
    [SerializeField] private GameObject _controlsScreen;
    [SerializeField] private GameObject _foundBubblesScreen;
    [SerializeField] private GameObject _sleepingOliveScreen;
    [SerializeField] private GameObject _creditsScreen;


    //Monobehaviors


    //Utils
    public void HideTitle()
    {
        _titleScreen.SetActive(false);
    }

    public void ShowControls()
    {
        _controlsScreen.SetActive(true);
    }

    public void HideControls()
    {
        _controlsScreen.SetActive(false);
    }

    public void ShowFoundBubblesScreen()
    {
        _foundBubblesScreen.SetActive(true);
    }

    public void HideFoundBubblesScreen()
    {
        _foundBubblesScreen.SetActive(false);
    }

    public void ShowOliveAsleepScreen()
    {
        _sleepingOliveScreen.SetActive(true);
    }

    public void HideOliveAsleepScreen()
    {
        _sleepingOliveScreen.SetActive(false);
    }

    public void ShowCredits()
    {
        _creditsScreen.SetActive(true);
    }

    public void HideCredits()
    {
        _creditsScreen.SetActive(false);
    }
}
