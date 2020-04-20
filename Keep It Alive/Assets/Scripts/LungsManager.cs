using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LungsManager : MonoBehaviour
{
    public static LungsManager instance;

    [Header("CONFIGURATION")]
    public float airMaxTime;
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
    public float currentAir;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        currentAir = airMaxTime;
        filling1 = renderer1.material;
        filling2 = renderer2.material;
        filling1.SetFloat("Vector1_B2746C0A", currentAir / airMaxTime);
        filling2.SetFloat("Vector1_B2746C0A", currentAir / airMaxTime);

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!HeartManager.instance.defeat)
        {
            if (!tracheaOpen || foodStucked)
            {
                currentAir -= Time.deltaTime;
                if (currentAir <= 0f)
                {
                    currentAir = 0f;
                    HeartManager.instance.TakeDamage(pvLossPerSecond * Time.deltaTime);
                }
            }
            else
            {
                if(currentAir <= 0f)
                {
                    if (!specificSoundSource.isPlaying)
                        specificSoundSource.PlayOneShot(AudioManager.instance.breath, AudioManager.instance.breathVolume);
                }
                    
                currentAir += Time.deltaTime;
                if (currentAir > airMaxTime)
                    currentAir = airMaxTime;
            }
            if (currentAir <= 0f || foodStucked)
            {
                if (!anim.GetBool("Danger"))
                    anim.SetBool("Danger", true);
            }
            else
            {
                if (anim.GetBool("Danger"))
                    anim.SetBool("Danger", false);
            }
            filling1.SetFloat("Vector1_B2746C0A", currentAir / airMaxTime);
            filling2.SetFloat("Vector1_B2746C0A", currentAir / airMaxTime);
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
