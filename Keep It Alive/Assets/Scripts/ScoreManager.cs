using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("COMPONENTS")]
    public TextMeshProUGUI endScore;
    TextMeshProUGUI scoreText;

    [Header("VARIABLES")]
    public bool scoring = true;
    float score;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (scoring)
        {
            score += Time.deltaTime;
            scoreText.text = ((int)score).ToString();
            endScore.text = ((int)score).ToString();
        }
    }
}
