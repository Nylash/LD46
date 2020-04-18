using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public static InteractManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void InteractWith(Organ organ)
    {
        switch (organ)
        {
            case Organ.lungs:
                break;
            case Organ.bladder:
                break;
            case Organ.trachea:
                LungsManager.instance.tracheaOpen = !LungsManager.instance.tracheaOpen;
                break;
            case Organ.mouth:
                break;
            case Organ.brain:
                break;
            case Organ.leftKidney:
                if (KidneyManager.instance.alarmLaunched && KidneyManager.instance.leftKidneyDying)
                {
                    KidneyManager.instance.currentInputNumber += 1;
                    if (KidneyManager.instance.currentInputNumber >= KidneyManager.instance.inputToBeChanged)
                        KidneyManager.instance.NewKidney();
                }
                break;
            case Organ.rightKidney:
                if (KidneyManager.instance.alarmLaunched && KidneyManager.instance.rightKidneyDying)
                {
                    KidneyManager.instance.currentInputNumber += 1;
                    if (KidneyManager.instance.currentInputNumber >= KidneyManager.instance.inputToBeChanged)
                        KidneyManager.instance.NewKidney();
                }
                break;
            case Organ.none:
                Debug.LogError("You shouldn't be there.");
                break;
        }
    }

    public enum Organ
    {
        lungs, bladder, trachea, mouth, brain, leftKidney, rightKidney, none
    }
}
