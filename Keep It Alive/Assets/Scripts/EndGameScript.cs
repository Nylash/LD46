using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    public Canvas end;

    InputMap inputMap;

    private void OnEnable() => inputMap.Gameplay.Enable();
    private void OnDisable() => inputMap.Gameplay.Disable();

    private void Awake()
    {
        inputMap = new InputMap();

        inputMap.Gameplay.Start.started += ctx => Replay();
        inputMap.Gameplay.Quit.started += ctx => SplashScreen();
    }

    void Replay()
    {
        if(end.enabled)
            SceneManager.LoadScene("MainLevel");
    }

    void SplashScreen()
    {
        if (end.enabled)
            SceneManager.LoadScene("SplashScene");
    }
}
