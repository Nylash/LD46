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
    Material filling1;
    Material filling2;

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
        filling1 = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().material;
        filling2 = transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().material;
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
        filling1.SetFloat("Vector1_B2746C0A", currentAir / airMaxTime);
        filling2.SetFloat("Vector1_B2746C0A", currentAir / airMaxTime);
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
        
    }
}
