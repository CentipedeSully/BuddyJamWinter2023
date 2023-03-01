using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;
using UnityEngine.SceneManagement;

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
        yield return new WaitForSeconds(3);
        UiManager.Instance.GetScreenManager().ShowFoundBubblesScreen();
        yield return new WaitForSeconds(3);
        while(Input.GetKeyDown(KeyCode.Space) == false )
        {
            yield return null;
        }

        UiManager.Instance.GetScreenManager().HideFoundBubblesScreen();
        UiManager.Instance.GetScreenManager().ShowOliveAsleepScreen();
        yield return new WaitForSeconds(3);
        while (Input.GetKeyDown(KeyCode.Space) == false)
        {
            yield return null;
        }

        UiManager.Instance.GetScreenManager().HideOliveAsleepScreen();
        UiManager.Instance.GetScreenManager().ShowCredits();
        yield return new WaitForSeconds(15);
        while (Input.GetKeyDown(KeyCode.Space) == false)
        {
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
