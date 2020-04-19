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
                if (LungsManager.instance.foodStucked)
                {
                    StartInteraction();
                    LungsManager.instance.currentInputNumber += 1;
                    if (LungsManager.instance.currentInputNumber >= LungsManager.instance.inputToBeUnstucked)
                        LungsManager.instance.UnstuckTrachea();
                }
                break;
            case Organ.bladder:
                StartInteraction();
                BladderManager.instance.peeing = !BladderManager.instance.peeing;
                break;
            case Organ.trachea:
                StartInteraction();
                LungsManager.instance.tracheaOpen = !LungsManager.instance.tracheaOpen;
                break;
            case Organ.mouth:
                StartInteraction();
                StomachManager.instance.mouthOpen = !StomachManager.instance.mouthOpen;
                break;
            case Organ.brain:
                StartInteraction();
                BrainManager.instance.ReduceStress();
                break;
            case Organ.leftKidney:
                if (KidneyManager.instance.alarmLaunched && KidneyManager.instance.leftKidneyDying)
                {
                    StartInteraction();
                    KidneyManager.instance.currentInputNumber += 1;
                    if (KidneyManager.instance.currentInputNumber >= KidneyManager.instance.inputToBeChanged)
                        KidneyManager.instance.NewKidney();
                }
                break;
            case Organ.rightKidney:
                if (KidneyManager.instance.alarmLaunched && KidneyManager.instance.rightKidneyDying)
                {
                    StartInteraction();
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

    void StartInteraction()
    {
        if (!PlayerManager.instance.animController.GetBool("Interacting"))
        {
            PlayerManager.instance.animController.SetTrigger("StartInteracting");
            PlayerManager.instance.animController.SetBool("Interacting", true);
        }
        PlayerManager.instance.controller.rb.velocity = Vector2.zero;
    }

    public enum Organ
    {
        lungs, bladder, trachea, mouth, brain, leftKidney, rightKidney, none
    }
}
