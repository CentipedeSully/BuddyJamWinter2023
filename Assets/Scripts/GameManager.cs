using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class GameManager : MonoSingleton<GameManager>
{
    //Declarations



    //Monos


    //Utils
    public void StartGame()
    {
        //Spawn Player
        PlayerObjectManager.Instance.SpawnPlayer();

        //Start FadeIn
        UiManager.Instance.GetScreenFadeController().FadeToTransparent(5);
    }

    public void EndGame()
    {
        StartCoroutine(CloseGame());
    }

    private IEnumerator CloseGame()
    {
        PlayerObjectManager.Instance.DisablePlayerControls();
        UiManager.Instance.GetScreenManager().ShowFoundBubblesScreen();
        while(Input.GetKeyDown(KeyCode.Space) == false )
        {
            yield return null;
        }

        UiManager.Instance.GetScreenManager().HideFoundBubblesScreen();
        UiManager.Instance.GetScreenManager().ShowOliveAsleepScreen();
        while (Input.GetKeyDown(KeyCode.Space) == false)
        {
            yield return null;
        }

        UiManager.Instance.GetScreenManager().HideOliveAsleepScreen();
        UiManager.Instance.GetScreenManager().ShowCredits();
    }


}
