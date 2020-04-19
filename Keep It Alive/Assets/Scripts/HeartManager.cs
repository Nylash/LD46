﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public static HeartManager instance;

    [Header("COMPONENTS")]
    Material filling;

    [Header("VARIABLES")]
    public float currentLife = 100f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        filling = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().material;
        filling.SetFloat("Vector1_B2746C0A", currentLife / 100);
    }

    public void TakeDamage(float amount)
    {
        currentLife -= amount;
        if (currentLife <= 0)
        {
            currentLife = 0f;
            print("DEFEAT");
        }
        filling.SetFloat("Vector1_B2746C0A", currentLife/100);
    }
}
