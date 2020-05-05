using UnityEngine;

public class BladderManager : MonoBehaviour
{
    public static BladderManager instance;

    [Header("CONFIGURATION")]
    public float maxCapacity;
    public float lossPerSecondWhenOpen;
    public float timeBeforeKillKidneyWhenFull;
    public float stomachSpeedUpEmptying;

    [Header("COMPONENTS")]
    public SpriteRenderer render;
    Material filling;
    Animator anim;

    [Header("VARIABLES")]
    public bool peeing;
    public bool full;
    public float currentCapacity;
    public float currentTimer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        filling = render.material;
        filling.SetFloat("Vector1_B2746C0A", currentCapacity / maxCapacity);

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!HeartManager.instance.defeat)
        {
            if (full)
            {
                currentTimer += Time.deltaTime;
                if (currentTimer >= timeBeforeKillKidneyWhenFull)
                {
                    KidneyManager.instance.KillKidney();
                    currentTimer = 0;
                }
            }
            if (peeing)
            {
                currentCapacity -= lossPerSecondWhenOpen * Time.deltaTime;
                if (currentCapacity <= 0f)
                {
                    currentCapacity = 0;
                    StomachManager.instance.speedEmptying = stomachSpeedUpEmptying;
                }
                else
                    StomachManager.instance.speedEmptying = 1;

            }
            else
                StomachManager.instance.speedEmptying = 1;
            filling.SetFloat("Vector1_B2746C0A", currentCapacity / maxCapacity);
            if (currentCapacity >= maxCapacity)
            {
                if (!anim.GetBool("Danger"))
                    anim.SetBool("Danger", true);
            }
            else
            {
                if (anim.GetBool("Danger"))
                    anim.SetBool("Danger", false);
            }
        }
    }

    public float TransfertToBladder(float amount)
    {
        float excess = 0f;
        currentCapacity += amount;
        if (currentCapacity >= maxCapacity)
        {
            if (!full)
            {
                full = true;
                currentTimer = 0;
            }
            excess = currentCapacity - maxCapacity;
            currentCapacity = maxCapacity;
        }
        else
            full = false;
        filling.SetFloat("Vector1_B2746C0A", currentCapacity / maxCapacity);
        return excess;
    }
}
