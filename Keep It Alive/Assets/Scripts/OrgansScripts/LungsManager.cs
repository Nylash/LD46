using UnityEngine;

public class LungsManager : MonoBehaviour
{
    public static LungsManager instance;

    [Header("CONFIGURATION")]
    public float maxCapacity;
    public float pvLossPerSecond;
    public int inputToBeUnstucked;

    [Header("COMPONENTS")]
    public SpriteRenderer renderer1;
    public SpriteRenderer renderer2;
    public Animator anim;
    public AudioSource specificSoundSource;
    Material filling1;
    Material filling2;

    [Header("VARIABLES")]
    public bool tracheaOpen = true;
    public bool foodStucked = false;
    public int currentInputNumber;
    public float currentCapacity;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        currentCapacity = maxCapacity;
        filling1 = renderer1.material;
        filling2 = renderer2.material;
        filling1.SetFloat("Vector1_B2746C0A", currentCapacity / maxCapacity);
        filling2.SetFloat("Vector1_B2746C0A", currentCapacity / maxCapacity);

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!HeartManager.instance.defeat)
        {
            if (!tracheaOpen || foodStucked)
            {
                currentCapacity -= Time.deltaTime;
                if (currentCapacity <= 0f)
                {
                    currentCapacity = 0f;
                    HeartManager.instance.TakeDamage(pvLossPerSecond * Time.deltaTime);
                }
            }
            else
            {
                if(currentCapacity <= 0f)
                {
                    if (!specificSoundSource.isPlaying)
                        specificSoundSource.PlayOneShot(AudioManager.instance.breath, AudioManager.instance.breathVolume);
                }
                    
                currentCapacity += Time.deltaTime;
                if (currentCapacity > maxCapacity)
                    currentCapacity = maxCapacity;
            }
            if (currentCapacity <= 0f || foodStucked)
            {
                if (!anim.GetBool("Danger"))
                    anim.SetBool("Danger", true);
            }
            else
            {
                if (anim.GetBool("Danger"))
                    anim.SetBool("Danger", false);
            }
            filling1.SetFloat("Vector1_B2746C0A", currentCapacity / maxCapacity);
            filling2.SetFloat("Vector1_B2746C0A", currentCapacity / maxCapacity);
        }
    }

    public void UnstuckTrachea()
    {
        foodStucked = false;
        currentInputNumber = 0;
        if (!specificSoundSource.isPlaying)
            specificSoundSource.PlayOneShot(AudioManager.instance.breath, AudioManager.instance.breathVolume);
    }
}
