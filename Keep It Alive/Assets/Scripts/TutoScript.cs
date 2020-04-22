using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutoScript : MonoBehaviour
{

    public bool tutoScene = false;
    public Canvas pause;
    public Canvas hintBind;
    public Sprite[] tutoSprites;
    public Image tutoRenderer;

    int index = 0;
    InputMap inputMap;

    private void OnEnable() => inputMap.Gameplay.Enable();
    private void OnDisable() => inputMap.Gameplay.Disable();

    private void Awake()
    {
        inputMap = new InputMap();

        inputMap.Gameplay.yAxis.started += ctx => Tuto(ctx.ReadValue<float>());
        inputMap.Gameplay.Menu.started += ctx => Menu();
        inputMap.Gameplay.Start.started += ctx => LoadGameFromTuto();
    }

    void Tuto(float ctx)
    {
        if (tutoScene)
        {
            if (ctx < 0)
                index++;
            if (ctx > 0)
                index--;
            if (index < 0)
                index = 0;
            if (index >= tutoSprites.Length)
                index = tutoSprites.Length - 1;
            tutoRenderer.sprite = tutoSprites[index];
        }
        else
        {
            if (pause.enabled)
            {
                if (ctx < 0)
                    index++;
                if (ctx > 0)
                    index--;
                if (index < 0)
                    index = 0;
                if (index >= tutoSprites.Length)
                    index = tutoSprites.Length - 1;
                tutoRenderer.sprite = tutoSprites[index];
                if (index != 0)
                {
                    if (!hintBind.enabled)
                        hintBind.enabled = true;
                }
                else
                {
                    if (hintBind.enabled)
                        hintBind.enabled = false;
                }
            }
        }
    }

    void Menu()
    {
        if (pause.enabled && !tutoScene)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("SplashScene");
        }
            
    }

    void LoadGameFromTuto()
    {
        if(tutoScene)
            SceneManager.LoadScene("MainLevel");
    }

    public void ResetTuto()
    {
        index = 0;
        tutoRenderer.sprite = tutoSprites[0];
        hintBind.enabled = false;
    }
}
