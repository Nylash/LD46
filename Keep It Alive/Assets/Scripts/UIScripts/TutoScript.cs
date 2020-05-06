using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutoScript : MonoBehaviour
{
    public static TutoScript instance;

    public bool tutoScene = false;
    public Canvas pause;
    public Canvas tuto;
    public Sprite[] tutoSprites;
    public Image tutoRenderer;

    int index = 0;
    InputMap inputMap;

    private void OnEnable() => inputMap.Gameplay.Enable();
    private void OnDisable() => inputMap.Gameplay.Disable();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        inputMap = new InputMap();

        inputMap.Gameplay.xAxis.started += ctx => Tuto(ctx.ReadValue<float>());
        inputMap.Gameplay.Start.started += ctx => LoadGameFromTuto();
    }

    public void Tuto(float ctx)
    {
        if (tuto.enabled)
        {
            if (ctx > 0)
                index++;
            if (ctx < 0)
                index--;
            if (index < 0)
                index = 0;
            if (index >= tutoSprites.Length)
                index = tutoSprites.Length - 1;
            tutoRenderer.sprite = tutoSprites[index];
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
        tuto.enabled = false;
    }
}
