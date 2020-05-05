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
    bool inStomach;
    SpriteRenderer render;

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
        int i = Random.Range(0, 2);
        if(i == 0)
            AudioManager.instance.foodSource.PlayOneShot(AudioManager.instance.eat1, AudioManager.instance.eat1Volume);
        else
            AudioManager.instance.foodSource.PlayOneShot(AudioManager.instance.eat2, AudioManager.instance.eat2Volume);
        render = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (inStomach)
        {
            if (render.color.a != PipesManager.instance.stomach.color.a)
                render.color = PipesManager.instance.stomach.color;
        }
        else
        {
            if (render.color.a != PipesManager.instance.mouth.color.a)
                render.color = PipesManager.instance.mouth.color;
        }
    }

    public void CheckTrachea()
    {
        if (LungsManager.instance.tracheaOpen)
        {
            StomachManager.instance.StartCoroutineFoodCD();
            LungsManager.instance.foodStucked = true;
            StomachManager.instance.mouthOpen = false;
            InteractManager.instance.mouthButton.SetBool("Close", true);
            InteractManager.instance.mouthButton.SetBool("Open", false);
            InteractManager.instance.mouthHint.sprite = InteractManager.instance.mouthClose;
            InteractManager.instance.lungsButton.SetTrigger("Open");
            InteractManager.instance.lungsHint.sprite = InteractManager.instance.lungsStuck;
            AudioManager.instance.foodSource.PlayOneShot(AudioManager.instance.cough, AudioManager.instance.coughVolume);
            Destroy(gameObject);
        }
        else
            inStomach = true;
    }

    public void StomachReached()
    {
        StomachManager.instance.StartCoroutineFoodCD();
        StomachManager.instance.AbsorbFood(currentGain);
        AudioManager.instance.foodSource.PlayOneShot(AudioManager.instance.burp, AudioManager.instance.burpVolume);
        Destroy(gameObject);
    }

    public enum Food
    {
        melon, chicken, cupcake
    }
}
