using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("COMPONENTS")]
    public Canvas pause;
    public Canvas start;
    public Canvas end;
    public Canvas tuto;
    public Button resumeButton;
    public TextMeshProUGUI startText;
    public TextMeshProUGUI endScore;
    public TextMeshProUGUI scoreText;
    InputMap inputMap;

    [Header("VARIABLES")]
    public bool scoring = true;
    public GameObject lastSelected;
    float score;

    private void OnEnable() => inputMap.Gameplay.Enable();
    private void OnDisable() => inputMap.Gameplay.Disable();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        inputMap = new InputMap();

        inputMap.Gameplay.Start.started += ctx => Replay();
        inputMap.Gameplay.Quit.started += ctx => SplashScreen();
    }

    private void Start()
    {
        StartCoroutine(StartScreenAnim());
    }

    IEnumerator StartScreenAnim()
    {
        HeartManager.instance.defeat = true;
        scoring = false;
        start.enabled = true;
        startText.text = "Ready ?";
        yield return new WaitForSeconds(1);
        startText.text = "Set";
        yield return new WaitForSeconds(1);
        startText.text = "Go !";
        yield return new WaitForSeconds(1);
        PlayerManager.instance.SwitchCamera();
        HeartManager.instance.defeat = false;
        scoring = true;
        start.enabled = false;
    }

    private void Update()
    {
        if (pause.enabled && !tuto.enabled)
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
        if (scoring)
        {
            score += Time.deltaTime;
            scoreText.text = ((int)score).ToString();
            endScore.text = ((int)score).ToString();
        }
    }

    public void Pause(bool pauseActivate)
    {
        pause.enabled = pauseActivate;
        Time.timeScale = (pauseActivate ? 0 : 1);
        if (!pauseActivate)
        {
            TutoScript.instance.ResetTuto();
            foreach (AudioSource item in AudioManager.instance.allSources)
            {
                if (item != AudioManager.instance.musicSource)
                    item.Play();
            }
            AudioManager.instance.musicSource.volume *= 2;
        }
        else
        {
            foreach (AudioSource item in AudioManager.instance.allSources)
            {
                if (item != AudioManager.instance.musicSource)
                    item.Pause();
            }
            AudioManager.instance.musicSource.volume /= 2;
            EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
        }
    }

    public void Menu()
    {
        if (pause.enabled)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("SplashScene");
        }
    }

    public void Tuto()
    {
        tuto.enabled = true;
        TutoScript.instance.Tuto(-1);
    }

    void Replay()
    {
        if (end.enabled)
            SceneManager.LoadScene("MainLevel");
    }

    void SplashScreen()
    {
        if (end.enabled)
            SceneManager.LoadScene("SplashScene");
    }
}
