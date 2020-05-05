using System.Collections;
using UnityEngine;
using TMPro;

public class StartScript : MonoBehaviour
{
    [Header("COMPONENTS")]
    public Canvas start;
    public TextMeshProUGUI startText;

    private void Start()
    {
        StartCoroutine(StartScreenAnim());
    }

    IEnumerator StartScreenAnim()
    {
        HeartManager.instance.defeat = true;
        ScoreManager.instance.scoring = false;
        start.enabled = true;
        startText.text = "Ready ?";
        yield return new WaitForSeconds(1);
        startText.text = "Set";
        yield return new WaitForSeconds(1);
        startText.text = "Go !";
        yield return new WaitForSeconds(1);
        PlayerManager.instance.SwitchCamera();
        HeartManager.instance.defeat = false;
        ScoreManager.instance.scoring = true;
        start.enabled = false;
    }
}
