using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public static HeartManager instance;

    [Header("COMPONENTS")]
    //public Text textTmp;

    [Header("VARIABLES")]
    float currentLife = 100f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        //textTmp.text = currentLife.ToString();
    }

    public void TakeDamage(float amount)
    {
        currentLife -= amount;
        if (currentLife <= 0)
        {
            currentLife = 0f;
            print("DEFEAT");
        }
        //else
            //textTmp.text = currentLife.ToString();
    }
}
