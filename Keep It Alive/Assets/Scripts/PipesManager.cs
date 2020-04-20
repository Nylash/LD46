
using UnityEngine;

public class PipesManager : MonoBehaviour
{
    public static PipesManager instance;

    [Header("COMPONENTS")]
    [Range(0f, 1f)] public float alphaWhenDesactivate;
    public SpriteRenderer nose;
    public SpriteRenderer lungs;
    public SpriteRenderer mouth;
    public SpriteRenderer stomach;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

        private void Update()
    {
        if (LungsManager.instance.tracheaOpen)
        {
            if(lungs.color.a != 1)
            {
                Color col = lungs.color;
                col.a = 1;
                lungs.color = col;
                lungs.sortingOrder = 0;
            }
            if (stomach.color.a != alphaWhenDesactivate)
            {
                Color col = stomach.color;
                col.a = alphaWhenDesactivate;
                stomach.color = col;
                stomach.sortingOrder = -1;
            }
        }
        else
        {
            if (stomach.color.a != 1)
            {
                Color col = stomach.color;
                col.a = 1;
                stomach.color = col;
                stomach.sortingOrder = 0;
            }
            if (lungs.color.a != alphaWhenDesactivate)
            {
                Color col = lungs.color;
                col.a = alphaWhenDesactivate;
                lungs.color = col;
                lungs.sortingOrder = -1;
            }
        }
        if (StomachManager.instance.mouthOpen)
        {
            if (mouth.color.a != 1)
            {
                Color col = mouth.color;
                col.a = 1;
                mouth.color = col;
                mouth.sortingOrder = -2;
                nose.sortingOrder = -3;
            }
            if (LungsManager.instance.tracheaOpen)
            {
                if(nose.color.a != 1)
                {
                    Color col = nose.color;
                    col.a = 1;
                    nose.color = col;
                }
            }
            else
            {
                if (nose.color.a != alphaWhenDesactivate)
                {
                    Color col = nose.color;
                    col.a = alphaWhenDesactivate;
                    nose.color = col;
                }
            }
        }
        else
        {
            if (mouth.color.a != alphaWhenDesactivate)
            {
                Color col = mouth.color;
                col.a = alphaWhenDesactivate;
                mouth.color = col;
                mouth.sortingOrder = -3;
                nose.sortingOrder = -2;
            }
            if (LungsManager.instance.tracheaOpen)
            {
                if (nose.color.a != 1)
                {
                    Color col = nose.color;
                    col.a = 1;
                    nose.color = col;
                }
            }
            else
            {
                if (nose.color.a != alphaWhenDesactivate)
                {
                    Color col = nose.color;
                    col.a = alphaWhenDesactivate;
                    nose.color = col;
                }
            }
        }
    }
}
