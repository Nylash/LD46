using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsActivator : MonoBehaviour
{
    [SerializeField] Button[] buttons;

    public void ActivateButtons()
    {
        foreach (Button item in buttons)
            item.enabled = true;
    }

    public void DesactivateButtons()
    {
        foreach (Button item in buttons)
            item.enabled = false;
    }
}
