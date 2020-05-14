using UnityEngine;

public class BoulangerieScript : MonoBehaviour
{
    public Animator splashScreenAnim;

    void DisplaySplashScreen()
    {
        splashScreenAnim.SetTrigger("Display");
    }
}
