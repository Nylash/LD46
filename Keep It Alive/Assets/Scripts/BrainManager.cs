using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainManager : MonoBehaviour
{
    public static BrainManager instance;

    [Header("CONFIGURATION")]
    public float gainPerSecond;
    public float firstCancerSpawnPercentage;
    public float secondCancerSpawnPercentage;
    public float thirdCancerSpawnPercentage;
    public float pvLossPerSecond;
    public float stressReductionPerInput;
    public GameObject cancerPrefab;
    public List<Transform> cancerPositions = new List<Transform>();

    [Header("COMPONENTS")]
    public SpriteRenderer render;
    Material filling;

    [Header("VARIABLES")]
    public float currentStress;
    GameObject firstCancer;
    GameObject secondCancer;
    GameObject thirdCancer;
    Transform firstCancerSpot;
    Transform secondCancerSpot;
    Transform thirdCancerSpot;
    public List<Transform> availableCancerPositions = new List<Transform>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        filling = render.material;
        filling.SetFloat("Vector1_B2746C0A", currentStress / 100);
        availableCancerPositions.AddRange(cancerPositions);
    }

    private void Update()
    {
        if (!HeartManager.instance.defeat)
        {
            currentStress += gainPerSecond * Time.deltaTime;
            if (currentStress >= firstCancerSpawnPercentage && firstCancer == null)
            {
                int index = Random.Range(0, availableCancerPositions.Count);
                firstCancer = Instantiate(cancerPrefab, availableCancerPositions[index].position, cancerPrefab.transform.rotation);
                firstCancerSpot = availableCancerPositions[index];
                availableCancerPositions.RemoveAt(index);
            }
            if (currentStress >= secondCancerSpawnPercentage && secondCancer == null)
            {
                int index = Random.Range(0, availableCancerPositions.Count);
                secondCancer = Instantiate(cancerPrefab, availableCancerPositions[index].position, cancerPrefab.transform.rotation);
                secondCancerSpot = availableCancerPositions[index];
                availableCancerPositions.RemoveAt(index);
            }
            if (currentStress >= thirdCancerSpawnPercentage && thirdCancer == null)
            {
                int index = Random.Range(0, availableCancerPositions.Count);
                thirdCancer = Instantiate(cancerPrefab, availableCancerPositions[index].position, cancerPrefab.transform.rotation);
                thirdCancerSpot = availableCancerPositions[index];
                availableCancerPositions.RemoveAt(index);
            }
            if (currentStress >= 100f)
            {
                currentStress = 100f;
                HeartManager.instance.TakeDamage(pvLossPerSecond * Time.deltaTime);
            }
            filling.SetFloat("Vector1_B2746C0A", currentStress / 100);
            if (currentStress != 0f)
                InteractManager.instance.brainButton.SetTrigger("Open");
            else
                InteractManager.instance.brainButton.SetTrigger("Close");
        }
    }

    public void ReduceStress()
    {
        currentStress -= stressReductionPerInput;
        if(currentStress < firstCancerSpawnPercentage && firstCancer != null)
        {
            Destroy(firstCancer);
            availableCancerPositions.Add(firstCancerSpot);
            firstCancerSpot = null;
        }
        if (currentStress < secondCancerSpawnPercentage && secondCancer != null)
        {
            Destroy(secondCancer);
            availableCancerPositions.Add(secondCancerSpot);
            secondCancerSpot = null;
        }
        if (currentStress < thirdCancerSpawnPercentage && thirdCancer != null)
        {
            Destroy(thirdCancer);
            availableCancerPositions.Add(thirdCancerSpot);
            thirdCancerSpot = null;
        }
        if (currentStress < 0f)
            currentStress = 0f;
        //textTmp.text = ((int)currentStress).ToString();
    }
}
