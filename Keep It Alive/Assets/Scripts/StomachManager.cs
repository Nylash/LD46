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
    Animator anim;

    [Header("VARIABLES")]
    public GameObject currentFood;
    public bool mouthOpen;
    public float currentCapacity;
    public float speedEmptying = 1;
    bool foodOnCD;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        filling = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().material;
        filling.SetFloat("Vector1_B2746C0A", currentCapacity / maxCapacity);

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!HeartManager.instance.defeat)
        {
            if (mouthOpen && !foodOnCD)
            {
                int foodType = Random.Range(0, 3);
                currentFood = Instantiate(foodPrefab, spawnFoodTransform.position, Quaternion.identity);
                currentFood.GetComponent<FoodScript>().currentFood = (FoodScript.Food)foodType;
                foodOnCD = true;
            }
            if (currentCapacity > 0)
            {
                float transferAmount = Time.deltaTime * speedEmptying;
                currentCapacity -= transferAmount;
                float excess = BladderManager.instance.TransfertToBladder(transferAmount);
                currentCapacity += excess;
            }
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
            if (currentCapacity >= maxCapacity || currentCapacity <= 0f)
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

    public void AbsorbFood(float gain)
    {
        currentCapacity += gain;
    }

    public void StartCoroutineFoodCD()
    {
        StartCoroutine(ResetFoodCD());
    }

    IEnumerator ResetFoodCD()
    {
        yield return new WaitForSeconds(timeBetweenFood);
        foodOnCD = false;
    }
}
