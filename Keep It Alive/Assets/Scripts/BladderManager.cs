using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladderManager : MonoBehaviour
{
    public static BladderManager instance;

    [Header("CONFIGURATION")]
    public float maxCapacity;
    public float lossPerSecondWhenOpen;

    [Header("VARIABLES")]
    public bool peeing;
    public float currentCapacity;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {
        if (peeing)
        {
            currentCapacity -= lossPerSecondWhenOpen * Time.deltaTime;
            if (currentCapacity <= 0f)
                currentCapacity = 0;
        }
    }

    public float TransfertToBladder(float amount)
    {
        float excess = 0f;
        currentCapacity += amount;
        if(currentCapacity > maxCapacity)
        {
            excess = currentCapacity - maxCapacity;
            currentCapacity = maxCapacity;
        }
        return excess;
    }
}
