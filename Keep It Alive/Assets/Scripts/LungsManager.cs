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
    public int inputToBeUnstucked;

    [Header("COMPONENTS")]
    //public Text textTmp;

    [Header("VARIABLES")]
    public bool tracheaOpen = true;
    public bool foodStucked = false;
    public int currentInputNumber;
    public float currentAir;
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
        //textTmp.text = ((int)((currentAir / airMaxTime) * 100)).ToString();
    }

    private void Update()
    {
        if (!tracheaOpen || foodStucked)
        {
            currentAir -= Time.deltaTime;
            if (currentAir <= 0f)
            {
                currentAir = 0f;
                HeartManager.instance.TakeDamage(pvLossPerSecond * Time.deltaTime);
            }
        }
        else
        {
            currentAir += Time.deltaTime;
            if (currentAir > airMaxTime)
                currentAir = airMaxTime;
        }
        //textTmp.text = ((int)((currentAir / airMaxTime) * 100)).ToString();
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

    public void UnstuckTrachea()
    {
        foodStucked = false;
        currentInputNumber = 0;
    }

    void Alarm()
    {
        print("ALERT LACK OXYGEN HAAAAAAAAALP");
    }
}
