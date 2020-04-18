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
            case Organ.esophagus:
                LungsManager.instance.esophagusOpen = !LungsManager.instance.esophagusOpen;
                break;
            case Organ.mouth:
                break;
            case Organ.brain:
                break;
            case Organ.kidney:
                break;
            case Organ.none:
                Debug.LogError("You shouldn't be there.");
                break;
        }
    }

    public enum Organ
    {
        lungs, bladder, esophagus, mouth, brain, kidney, none
    }
}
