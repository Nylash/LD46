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
                    if (!PlayerManager.instance.animController.GetBool("Interacting"))
                    {
                        PlayerManager.instance.animController.SetTrigger("StartInteracting");
                        PlayerManager.instance.animController.SetBool("Interacting", true);
                    }
                    LungsManager.instance.currentInputNumber += 1;
                    if (LungsManager.instance.currentInputNumber >= LungsManager.instance.inputToBeUnstucked)
                        LungsManager.instance.UnstuckTrachea();
                }
                break;
            case Organ.bladder:
                if (!PlayerManager.instance.animController.GetBool("Interacting"))
                {
                    PlayerManager.instance.animController.SetTrigger("StartInteracting");
                    PlayerManager.instance.animController.SetBool("Interacting", true);
                }
                BladderManager.instance.peeing = !BladderManager.instance.peeing;
                break;
            case Organ.trachea:
                if (!PlayerManager.instance.animController.GetBool("Interacting"))
                {
                    PlayerManager.instance.animController.SetTrigger("StartInteracting");
                    PlayerManager.instance.animController.SetBool("Interacting", true);
                }
                LungsManager.instance.tracheaOpen = !LungsManager.instance.tracheaOpen;
                break;
            case Organ.mouth:
                if (!PlayerManager.instance.animController.GetBool("Interacting"))
                {
                    PlayerManager.instance.animController.SetTrigger("StartInteracting");
                    PlayerManager.instance.animController.SetBool("Interacting", true);
                }
                StomachManager.instance.mouthOpen = !StomachManager.instance.mouthOpen;
                break;
            case Organ.brain:
                if (!PlayerManager.instance.animController.GetBool("Interacting"))
                {
                    PlayerManager.instance.animController.SetTrigger("StartInteracting");
                    PlayerManager.instance.animController.SetBool("Interacting", true);
                }
                BrainManager.instance.ReduceStress();
                break;
            case Organ.leftKidney:
                if (KidneyManager.instance.alarmLaunched && KidneyManager.instance.leftKidneyDying)
                {
                    if (!PlayerManager.instance.animController.GetBool("Interacting"))
                    {
                        PlayerManager.instance.animController.SetTrigger("StartInteracting");
                        PlayerManager.instance.animController.SetBool("Interacting", true);
                    }
                    KidneyManager.instance.currentInputNumber += 1;
                    if (KidneyManager.instance.currentInputNumber >= KidneyManager.instance.inputToBeChanged)
                        KidneyManager.instance.NewKidney();
                }
                break;
            case Organ.rightKidney:
                if (KidneyManager.instance.alarmLaunched && KidneyManager.instance.rightKidneyDying)
                {
                    if (!PlayerManager.instance.animController.GetBool("Interacting"))
                    {
                        PlayerManager.instance.animController.SetTrigger("StartInteracting");
                        PlayerManager.instance.animController.SetBool("Interacting", true);
                    }
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
