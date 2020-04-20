using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public static InteractManager instance;

    [Header("CONFIGURATIONS")]
    public Animator brainButton;
    public Animator lungsButton;
    public Animator rightKidneyButton;
    public Animator leftKidneyButton;
    public Animator tracheaButton;
    public Animator mouthButton;
    public Animator bladderButton;
    public SpriteRenderer mouthHint;
    public SpriteRenderer tracheaHint;
    public SpriteRenderer bladderHint;
    public Sprite mouthOpen;
    public Sprite mouthClose;
    public Sprite tracheaOpen;
    public Sprite tracheaClose;
    public Sprite bladderOpen;
    public Sprite bladderClose;

    [Header("VARIABLES")]
    public bool foodInStomach;

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
                    lungsButton.SetTrigger("Interact");
                    StartInteraction();
                    LungsManager.instance.currentInputNumber += 1;
                    if (LungsManager.instance.currentInputNumber >= LungsManager.instance.inputToBeUnstucked)
                    {
                        LungsManager.instance.UnstuckTrachea();
                        lungsButton.SetTrigger("Close");
                    }
                        
                }
                break;
            case Organ.bladder:
                StartInteraction();
                if (BladderManager.instance.peeing)
                {
                    BladderManager.instance.peeing = false;
                    bladderButton.SetBool("Close", true);
                    bladderButton.SetBool("Open", false);
                    bladderHint.sprite = bladderClose;
                }
                else
                {
                    BladderManager.instance.peeing = true;
                    bladderButton.SetBool("Close", false);
                    bladderButton.SetBool("Open", true);
                    bladderHint.sprite = bladderOpen;
                }
                break;
            case Organ.trachea:
                if (!LungsManager.instance.foodStucked && !foodInStomach)
                {
                    StartInteraction();
                    if (LungsManager.instance.tracheaOpen)
                    {
                        LungsManager.instance.tracheaOpen = false;
                        tracheaButton.SetBool("Close", true);
                        tracheaButton.SetBool("Open", false);
                        tracheaHint.sprite = tracheaClose;
                    }
                    else
                    {
                        LungsManager.instance.tracheaOpen = true;
                        tracheaButton.SetBool("Close", false);
                        tracheaButton.SetBool("Open", true);
                        tracheaHint.sprite = tracheaOpen;
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
                        mouthButton.SetBool("Close", true);
                        mouthButton.SetBool("Open", false);
                        mouthHint.sprite = mouthClose;
                    }
                    else
                    {
                        StomachManager.instance.mouthOpen = true;
                        mouthButton.SetBool("Close", false);
                        mouthButton.SetBool("Open", true);
                        mouthHint.sprite = mouthOpen;
                    }
                }
                break;
            case Organ.brain:
                brainButton.SetTrigger("Interact");
                StartInteraction();
                BrainManager.instance.ReduceStress();
                break;
            case Organ.leftKidney:
                if (KidneyManager.instance.leftKidneyDying)
                {
                    leftKidneyButton.SetTrigger("Interact");
                    StartInteraction();
                    KidneyManager.instance.currentInputNumberLeft += 1;
                    if (KidneyManager.instance.currentInputNumberLeft >= KidneyManager.instance.inputToBeChanged)
                        KidneyManager.instance.NewLeftKidney();                        
                }
                break;
            case Organ.rightKidney:
                if (KidneyManager.instance.rightKidneyDying)
                {
                    rightKidneyButton.SetTrigger("Interact");
                    StartInteraction();
                    KidneyManager.instance.currentInputNumberRight += 1;
                    if (KidneyManager.instance.currentInputNumberRight >= KidneyManager.instance.inputToBeChanged)
                        KidneyManager.instance.NewRightKidney();
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
