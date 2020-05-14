using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    [Header("CONFIGURATION")]
    public GameObject firstSelected;

    [Header("VARIABLES")]
    public GameObject lastSelected;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != lastSelected)
        {
            if (lastSelected)
                lastSelected.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            lastSelected = EventSystem.current.currentSelectedGameObject;
            lastSelected.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            lastSelected.GetComponent<Animator>().SetTrigger("Selected");
        }
    }

    public void OnePlayerGame()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
