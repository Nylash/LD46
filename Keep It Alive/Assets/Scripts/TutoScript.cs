using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoScript : MonoBehaviour
{
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
    }

    void Tuto(float ctx)
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
            if(index != 0)
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
        else
        {
            if (index != 0)
            {
                index = 0;
                tutoRenderer.sprite = tutoSprites[0];
                hintBind.enabled = false;
            }   
        }
    }
}
