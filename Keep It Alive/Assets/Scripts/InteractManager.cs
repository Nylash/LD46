using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public static InteractManager instance;

    [Header("CONFIGURATIONS")]
    public Animator brain;
    public Animator lungs;
    public Animator rightKidney;
    public Animator leftKidney;
    public Animator trachea;
    public Animator mouth;
    public Animator bladder;

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
                    lungs.SetTrigger("Interact");
                    StartInteraction();
                    LungsManager.instance.currentInputNumber += 1;
                    if (LungsManager.instance.currentInputNumber >= LungsManager.instance.inputToBeUnstucked)
                        LungsManager.instance.UnstuckTrachea();
                }
                break;
            case Organ.bladder:
                StartInteraction();
                if (BladderManager.instance.peeing)
                {
                    BladderManager.instance.peeing = false;
                    bladder.SetBool("Close", true);
                    bladder.SetBool("Open", false);
                }
                else
                {
                    BladderManager.instance.peeing = true;
                    bladder.SetBool("Close", false);
                    bladder.SetBool("Open", true);
                }
                break;
            case Organ.trachea:
                if (!LungsManager.instance.foodStucked && StomachManager.instance.currentFood == null)
                {
                    StartInteraction();
                    if (LungsManager.instance.tracheaOpen)
                    {
                        LungsManager.instance.tracheaOpen = false;
                        trachea.SetBool("Close", true);
                        trachea.SetBool("Open", false);
                    }
                    else
                    {
                        LungsManager.instance.tracheaOpen = true;
                        trachea.SetBool("Close", false);
                        trachea.SetBool("Open", true);
                    }
                }
                break;
            case Organ.mouth:
                if (!LungsManager.instance.foodStucked && StomachManager.instance.currentFood == null)
                {
                    StartInteraction();
                    if (StomachManager.instance.mouthOpen)
                    {
                        StomachManager.instance.mouthOpen = false;
                        mouth.SetBool("Close", true);
                        mouth.SetBool("Open", false);
                    }
                    else
                    {
                        StomachManager.instance.mouthOpen = true;
                        mouth.SetBool("Close", false);
                        mouth.SetBool("Open", true);
                    }
                }
                break;
            case Organ.brain:
                brain.SetTrigger("Interact");
                StartInteraction();
                BrainManager.instance.ReduceStress();
                break;
            case Organ.leftKidney:
                if (KidneyManager.instance.alarmLaunched && KidneyManager.instance.leftKidneyDying)
                {
                    leftKidney.SetTrigger("Interact");
                    StartInteraction();
                    KidneyManager.instance.currentInputNumber += 1;
                    if (KidneyManager.instance.currentInputNumber >= KidneyManager.instance.inputToBeChanged)
                        KidneyManager.instance.NewKidney();
                }
                break;
            case Organ.rightKidney:
                if (KidneyManager.instance.alarmLaunched && KidneyManager.instance.rightKidneyDying)
                {
                    rightKidney.SetTrigger("Interact");
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
