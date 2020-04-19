using UnityEngine;

public class EndInteract : MonoBehaviour
{
    public void EndInteraction()
    {
        PlayerManager.instance.animController.SetBool("Interacting", false);
    }
}
