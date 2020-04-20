using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladderManager : MonoBehaviour
{
    public static BladderManager instance;

    [Header("CONFIGURATION")]
    public float maxCapacity;
    public float lossPerSecondWhenOpen;
    public float timeBeforeKillKidneyWhenFull;

    [Header("COMPONENTS")]
    Material filling;

    [Header("VARIABLES")]
    public bool peeing;
    public bool full;
    public float currentCapacity;
    public float currentTimer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        filling = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (full)
        {
            currentTimer += Time.deltaTime;
            if(currentTimer >= timeBeforeKillKidneyWhenFull)
            {
                KidneyManager.instance.KillKidney();
                currentTimer = 0;
            }
        }
        if (peeing)
        {
            currentCapacity -= lossPerSecondWhenOpen * Time.deltaTime;
            if (currentCapacity <= 0f)
                currentCapacity = 0;
        }
        filling.SetFloat("Vector1_B2746C0A", currentCapacity / maxCapacity);
    }

    public float TransfertToBladder(float amount)
    {
        float excess = 0f;
        currentCapacity += amount;
        if (currentCapacity >= maxCapacity)
        {
            if (!full)
            {
                full = true;
                currentTimer = 0;
            }
            excess = currentCapacity - maxCapacity;
            currentCapacity = maxCapacity;
        }
        else
            full = false;
        filling.SetFloat("Vector1_B2746C0A", currentCapacity / maxCapacity);
        return excess;
    }
}
