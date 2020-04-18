using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LungsManager : MonoBehaviour
{
    public static LungsManager instance;

    [Header("CONFIGURATION")]
    public float airMaxCapacity;
    public float airLossPerSecond;
    public float airGainPerSecond;
    public float pvLossPerSecond;

    [Header("COMPONENTS")]
    public Text textTmp;

    [Header("VARIABLES")]
    public bool esophagusOpen = true;
    float currentAir;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        currentAir = airMaxCapacity;
        textTmp.text = currentAir.ToString();
        InvokeRepeating("UpdateAir", 1f, 1f);
    }

    void UpdateAir()
    {
        if (esophagusOpen)
        {
            currentAir += airGainPerSecond;
            if (currentAir > airMaxCapacity)
                currentAir = airMaxCapacity;
        }
        else
        {
            currentAir -= airLossPerSecond;
            if (currentAir <= 0f)
            {
                currentAir = 0f;
                HeartManager.instance.TakeDamage(pvLossPerSecond);
            }
        }
        textTmp.text = currentAir.ToString();
    }
}
