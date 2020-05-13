using UnityEngine;
using UnityEngine.UI;

public class ButtonsActivator : MonoBehaviour
{
    public Button[] buttons;

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
