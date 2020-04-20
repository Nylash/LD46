using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KidneyManager : MonoBehaviour
{
    public static KidneyManager instance;

    [Header("CONFIGURATION")]
    public float pvLossPerSecond;
    public int inputToBeChanged;

    [Header("VARIABLES")]
    public int currentInputNumber;
    public bool rightKidneyDying;
    public bool leftKidneyDying;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {
        if (!HeartManager.instance.defeat)
        {
            if (leftKidneyDying)
                HeartManager.instance.TakeDamage(pvLossPerSecond * Time.deltaTime);
            if (rightKidneyDying)
                HeartManager.instance.TakeDamage(pvLossPerSecond * Time.deltaTime);
        }
    }

    public void KillKidney()
    {
        if (leftKidneyDying && rightKidneyDying)
            return;
        if (leftKidneyDying)
        {
            rightKidneyDying = true;
            InteractManager.instance.rightKidneyButton.SetTrigger("Open");
            return;
        }
        if(rightKidneyDying)
        {
            leftKidneyDying = true;
            InteractManager.instance.leftKidneyButton.SetTrigger("Open");
            return;
        }
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            leftKidneyDying = true;
            InteractManager.instance.leftKidneyButton.SetTrigger("Open");
        }
        else
        {
            rightKidneyDying = true;
            InteractManager.instance.rightKidneyButton.SetTrigger("Open");
        }
    }

    public void NewKidney()
    {
        currentInputNumber = 0;
        leftKidneyDying = false;
        rightKidneyDying = false;
    }
}
