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
    public AudioSource brainSource;
    public AudioSource lungsSource;
    public AudioSource rightKidneySource;
    public AudioSource leftKidneySource;
    public AudioSource tracheaSource;
    public AudioSource mouthSource;
    public AudioSource bladderSource;
    public SpriteRenderer mouthHint;
    public SpriteRenderer tracheaHint;
    public SpriteRenderer bladderHint;
    public SpriteRenderer lungsHint;
    public Sprite mouthOpen;
    public Sprite mouthClose;
    public Sprite tracheaOpen;
    public Sprite tracheaClose;
    public Sprite bladderOpen;
    public Sprite bladderClose;
    public Sprite lungsStuck;
    public Sprite lungsOk;

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
                    PlayBuzzSound(lungsSource);
                    if (LungsManager.instance.currentInputNumber >= LungsManager.instance.inputToBeUnstucked)
                    {
                        lungsHint.sprite = lungsOk;
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
                    PlayCloseSound(bladderSource);
                }
                else
                {
                    BladderManager.instance.peeing = true;
                    bladderButton.SetBool("Close", false);
                    bladderButton.SetBool("Open", true);
                    bladderHint.sprite = bladderOpen;
                    PlayOpenSound(bladderSource);
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
                        LungsManager.instance.anim.SetBool("Off", true);
                        PlayCloseSound(tracheaSource);
                    }
                    else
                    {
                        LungsManager.instance.tracheaOpen = true;
                        tracheaButton.SetBool("Close", false);
                        tracheaButton.SetBool("Open", true);
                        tracheaHint.sprite = tracheaOpen;
                        LungsManager.instance.anim.SetBool("Off", false);
                        PlayOpenSound(tracheaSource);
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
                        PlayCloseSound(mouthSource);
                    }
                    else
                    {
                        StomachManager.instance.mouthOpen = true;
                        mouthButton.SetBool("Close", false);
                        mouthButton.SetBool("Open", true);
                        mouthHint.sprite = mouthOpen;
                        PlayOpenSound(mouthSource);
                    }
                }
                break;
            case Organ.brain:
                brainButton.SetTrigger("Interact");
                StartInteraction();
                BrainManager.instance.ReduceStress();
                PlayBuzzSound(brainSource);
                break;
            case Organ.leftKidney:
                if (KidneyManager.instance.leftKidneyDying)
                {
                    leftKidneyButton.SetTrigger("Interact");
                    StartInteraction();
                    KidneyManager.instance.currentInputNumberLeft += 1;
                    PlayBuzzSound(leftKidneySource);
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
                    PlayBuzzSound(rightKidneySource);
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

    void PlayOpenSound(AudioSource source)
    {
        source.PlayOneShot(AudioManager.instance.leverOpen, AudioManager.instance.leverOpenVolume);
    }

    void PlayCloseSound(AudioSource source)
    {
        source.PlayOneShot(AudioManager.instance.leverClose, AudioManager.instance.leverCloseVolume);
    }

    void PlayBuzzSound(AudioSource source)
    {
        source.PlayOneShot(AudioManager.instance.buzz, AudioManager.instance.buzzVolume);
    }

    public enum Organ
    {
        lungs, bladder, trachea, mouth, brain, leftKidney, rightKidney, none
    }
}
