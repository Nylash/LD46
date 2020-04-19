using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    [Header("CONFIGURATION")]
    public float gainWithMelon;
    public float gainWithChicken;
    public float gainWithCupcake;
    public Sprite melonSprite;
    public Sprite chickenSprite;
    public Sprite cupcakeSprite;

    [Header("VARIABLES")]
    public Food currentFood;
    public float currentGain;

    private void Start()
    {
        switch (currentFood)
        {
            case Food.melon:
                GetComponent<SpriteRenderer>().sprite = melonSprite;
                currentGain = gainWithMelon;
                break;
            case Food.chicken:
                GetComponent<SpriteRenderer>().sprite = chickenSprite;
                currentGain = gainWithChicken;
                break;
            case Food.cupcake:
                GetComponent<SpriteRenderer>().sprite = cupcakeSprite;
                currentGain = gainWithCupcake;
                break;
        }
    }

    public void CheckTrachea()
    {
        if (LungsManager.instance.tracheaOpen)
        {
            LungsManager.instance.foodStucked = true;
            StomachManager.instance.mouthOpen = false;
            Destroy(gameObject);
        }
    }

    public void StomachReached()
    {
        StomachManager.instance.AbsorbFood(currentGain);
        Destroy(gameObject);
    }

    public enum Food
    {
        melon, chicken, cupcake
    }
}
