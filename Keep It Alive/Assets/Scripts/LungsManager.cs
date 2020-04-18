using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LungsManager : MonoBehaviour
{
    public static LungsManager instance;

    [Header("CONFIGURATION")]
    public float airMaxTime;
    public float pvLossPerSecond;
    public float percentageAlarm;

    [Header("COMPONENTS")]
    public Text textTmp;

    [Header("VARIABLES")]
    public bool tracheaOpen = true;
    float currentAir;
    bool alarmLaunched;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        currentAir = airMaxTime;
        textTmp.text = ((int)((currentAir / airMaxTime) * 100)).ToString();
    }

    private void Update()
    {
        if (tracheaOpen)
        {
            currentAir += Time.deltaTime;
            if (currentAir > airMaxTime)
                currentAir = airMaxTime;
        }
        else
        {
            currentAir -= Time.deltaTime;
            if (currentAir <= 0f)
            {
                currentAir = 0f;
                HeartManager.instance.TakeDamage(pvLossPerSecond * Time.deltaTime);
            }
        }
        textTmp.text = ((int)((currentAir / airMaxTime) * 100)).ToString();
        if (!alarmLaunched)
        {
            if (((currentAir / airMaxTime) * 100) <= percentageAlarm)
            {
                alarmLaunched = true;
                InvokeRepeating("Alarm", 0, .5f);
            }
        }
        else
        {
            if (((currentAir / airMaxTime) * 100) > percentageAlarm)
            {
                alarmLaunched = false;
                CancelInvoke("Alarm");
            }
        }
        
    }

    void Alarm()
    {
        print("ALERT LACK OXYGEN HAAAAAAAAALP");
    }
}
