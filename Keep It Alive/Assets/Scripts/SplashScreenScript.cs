﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenScript : MonoBehaviour
{
    InputMap inputMap;

    private void OnEnable() => inputMap.Gameplay.Enable();
    private void OnDisable() => inputMap.Gameplay.Disable();

    private void Awake()
    {
        Cursor.visible = false;

        inputMap = new InputMap();

        inputMap.Gameplay.Start.started += ctx => SceneManager.LoadScene("Tutorial");
        inputMap.Gameplay.Quit.started += ctx => Application.Quit();
    }
}
