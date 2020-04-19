using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KidneyManager : MonoBehaviour
{
    public static KidneyManager instance;

    [Header("CONFIGURATION")]
    public float minTimeBeforeEnd;
    public float maxTimeBeforeEnd;
    public float pvLossPerSecond;
    public float percentageAlarm;
    public int inputToBeChanged;

    [Header("COMPONENTS")]
    //public Text textTmp;

    [Header("VARIABLES")]
    public int currentInputNumber;
    public bool alarmLaunched;
    public bool rightKidneyDying;
    public bool leftKidneyDying;
    public float currentMaxTime;
    public float currentTimer;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        currentMaxTime = Random.Range(minTimeBeforeEnd, maxTimeBeforeEnd);
        currentTimer = currentMaxTime;
        //textTmp.text = ((int)((currentTimer / currentMaxTime) * 100)).ToString();
    }

    private void Update()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0f)
        {
            currentTimer = 0f;
            HeartManager.instance.TakeDamage(pvLossPerSecond * Time.deltaTime);
        }
        //textTmp.text = ((int)((currentTimer / currentMaxTime)*100)).ToString();
        if((((currentTimer / currentMaxTime) * 100) <= percentageAlarm) && !alarmLaunched)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
                leftKidneyDying = true;
            else
                rightKidneyDying = true;
            alarmLaunched = true;
            InvokeRepeating("Alarm", 0, .5f);
        }
    }

    public void NewKidney()
    {
        CancelInvoke("Alarm");
        alarmLaunched = false;
        currentMaxTime = Random.Range(minTimeBeforeEnd, maxTimeBeforeEnd);
        currentTimer = currentMaxTime;
        //textTmp.text = ((int)((currentTimer / currentMaxTime) * 100)).ToString();
        currentInputNumber = 0;
        leftKidneyDying = false;
        rightKidneyDying = false;
    }

    void Alarm()
    {
        if(leftKidneyDying)
            print("ALERT LEFT KIDNEY DYIIIIING");
        else
            print("ALERT RIGHT KIDNEY DYIIIIING");
    }
}
