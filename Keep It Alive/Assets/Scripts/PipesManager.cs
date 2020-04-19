
using UnityEngine;

public class PipesManager : MonoBehaviour
{
    [Header("COMPONENTS")]
    [Range(0f, 1f)] public float alphaWhenDesactivate;
    public SpriteRenderer nose;
    public SpriteRenderer lungs;
    public SpriteRenderer mouth;
    public SpriteRenderer stomach;

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
                Color col2 = stomach.color;
                col2.a = alphaWhenDesactivate;
                stomach.color = col2;
                stomach.sortingOrder = -1;
            }
            if(nose.color.a != 1)
            {
                Color col = nose.color;
                col.a = 1;
                nose.color = col; 
                if(!StomachManager.instance.mouthOpen)
                    nose.sortingOrder = -2;
                else
                    nose.sortingOrder = -3;
            }
            if (!StomachManager.instance.mouthOpen)
            {
                if (mouth.color.a != alphaWhenDesactivate)
                {

                    Color col2 = mouth.color;
                    col2.a = alphaWhenDesactivate;
                    mouth.color = col2;
                    mouth.sortingOrder = -3;
                    nose.sortingOrder = -2;
                }
            }
            else
            {
                if (mouth.color.a != 1)
                {
                    Color col2 = mouth.color;
                    col2.a = 1;
                    mouth.color = col2;
                    mouth.sortingOrder = -2;
                }
            }
        }
        else
        {
            if(lungs.color.a != alphaWhenDesactivate)
            {
                Color col = lungs.color;
                col.a = alphaWhenDesactivate;
                lungs.color = col;
                lungs.sortingOrder = -1;
                Color col2 = nose.color;
                col2.a = alphaWhenDesactivate;
                nose.color = col2;
                nose.sortingOrder = -3;
            }
            if (StomachManager.instance.mouthOpen)
            {
                if(stomach.color.a != 1)
                {
                    Color col = mouth.color;
                    col.a = 1;
                    mouth.color = col;
                    mouth.sortingOrder = -2;
                    Color col2 = stomach.color;
                    col2.a = 1;
                    stomach.color = col2;
                    stomach.sortingOrder = 0;
                }
            }
            else
            {
                if (stomach.color.a != alphaWhenDesactivate)
                {
                    Color col = mouth.color;
                    col.a = alphaWhenDesactivate;
                    mouth.color = col;
                    mouth.sortingOrder = -3;
                    Color col2 = stomach.color;
                    col2.a = alphaWhenDesactivate;
                    stomach.color = col2;
                    stomach.sortingOrder = -1;
                }
            }
        }
    }
}
