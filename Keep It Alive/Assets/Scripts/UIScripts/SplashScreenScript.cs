using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SplashScreenScript : MonoBehaviour
{

    InputAction anyButton;

    private void Awake()
    {
        Cursor.visible = false;
    }

    void EnableLoading()
    {
        anyButton = new InputAction(binding: "/*/<button>");
        anyButton.Enable();
        anyButton.started += ctx => LoadGame();
    }

    void LoadGame()
    {
        anyButton.Disable();
        SceneManager.LoadScene("MainMenu");
    }
}
