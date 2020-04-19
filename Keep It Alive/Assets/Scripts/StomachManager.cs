using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StomachManager : MonoBehaviour
{
    public static StomachManager instance;

    [Header("CONFIGURATION")]
    public float maxCapacity;
    public float timeBetweenFood;
    public GameObject foodPrefab;
    public Transform spawnFoodTransform;
    public float pvLossPerSecond;

    [Header("COMPONENTS")]
    Material filling;

    [Header("VARIABLES")]
    public GameObject currentFood;
    public bool mouthOpen;
    public float currentCapacity;
    bool foodOnCD;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        currentCapacity = maxCapacity / 2;
        filling = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (mouthOpen && !foodOnCD)
        {
            int foodType = Random.Range(0, 3);
            currentFood = Instantiate(foodPrefab, spawnFoodTransform.position, Quaternion.identity);
            currentFood.GetComponent<FoodScript>().currentFood = (FoodScript.Food)foodType;
            foodOnCD = true;
            Invoke("ResetFoodCD", timeBetweenFood);
        }
        float transferAmount = Time.deltaTime;
        currentCapacity -= transferAmount;
        float excess = BladderManager.instance.TransfertToBladder(transferAmount);
        currentCapacity += excess;
        if (currentCapacity <= 0f)
        {
            currentCapacity = 0f;
            HeartManager.instance.TakeDamage(pvLossPerSecond * Time.deltaTime);
        }
        if (currentCapacity >= maxCapacity)
        {
            currentCapacity = maxCapacity;
            HeartManager.instance.TakeDamage(pvLossPerSecond * Time.deltaTime);
        }
        filling.SetFloat("Vector1_B2746C0A", currentCapacity / maxCapacity);
    }

    public void AbsorbFood(float gain)
    {
        currentCapacity += gain;
    }

    void ResetFoodCD()
    {
        foodOnCD = false;
    }
}
