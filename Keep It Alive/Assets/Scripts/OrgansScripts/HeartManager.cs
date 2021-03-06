﻿using UnityEngine;
using UnityEngine.EventSystems;

public class HeartManager : MonoBehaviour
{
    public static HeartManager instance;

    [Header("COMPONENTS")]
    public GameObject cam1;
    public GameObject cam2;
    public Canvas endScreen;
    public SpriteRenderer spriteRender;
    AudioSource source;
    Material filling;
    Animator anim;

    [Header("VARIABLES")]
    public float currentLife = 100f;
    public bool defeat;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        filling = spriteRender.material;
        filling.SetFloat("Vector1_B2746C0A", currentLife / 100);

        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        source.volume = AudioManager.instance.heartVolumeSafe;
    }

    public void TakeDamage(float amount)
    {
        ShakeScreenManager.instance.ShakeScreen();
        anim.SetTrigger("Damage");
        currentLife -= amount;
        if (currentLife <= 0)
        {
            currentLife = 0f;
            UIManager.instance.scoring = false;
            endScreen.enabled = true;
            endScreen.gameObject.GetComponent<ButtonsActivator>().ActivateButtons();
            EventSystem.current.SetSelectedGameObject(GameObject.FindGameObjectWithTag("UI_Replay"));
            defeat = true;
            cam1.SetActive(false);
            cam2.SetActive(true);
        }
        filling.SetFloat("Vector1_B2746C0A", currentLife/100);
    }

    public void VolumeSafe()
    {
        source.volume = AudioManager.instance.heartVolumeSafe;
    }

    public void VolumeDanger()
    {
        source.volume = AudioManager.instance.heartVolumeDanger;
    }
}
