using UnityEngine;

public class KidneyManager : MonoBehaviour
{
    public static KidneyManager instance;

    [Header("CONFIGURATION")]
    public float pvLossPerSecond;
    public int inputToBeChanged;

    [Header("COMPONENTS")]
    public Animator animL;
    public Animator animR;

    [Header("VARIABLES")]
    public int currentInputNumberLeft;
    public int currentInputNumberRight;
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
            animR.SetBool("Danger", true);
            return;
        }
        if(rightKidneyDying)
        {
            leftKidneyDying = true;
            InteractManager.instance.leftKidneyButton.SetTrigger("Open");
            animL.SetBool("Danger", true);
            return;
        }
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            leftKidneyDying = true;
            InteractManager.instance.leftKidneyButton.SetTrigger("Open");
            animL.SetBool("Danger", true);
        }
        else
        {
            rightKidneyDying = true;
            InteractManager.instance.rightKidneyButton.SetTrigger("Open");
            animR.SetBool("Danger", true);
        }
    }

    public void NewRightKidney()
    {
        currentInputNumberRight = 0;
        rightKidneyDying = false;
        InteractManager.instance.rightKidneyButton.SetTrigger("Close");
        animR.SetBool("Danger", false);
        BladderManager.instance.currentTimer = 0;
    }

    public void NewLeftKidney()
    {
        currentInputNumberLeft = 0;
        leftKidneyDying = false;
        InteractManager.instance.leftKidneyButton.SetTrigger("Close");
        animL.SetBool("Danger", false);
        BladderManager.instance.currentTimer = 0;
    }
}
