using System.Collections.Generic;
using UnityEngine;

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
    Animator anim;

    [Header("VARIABLES")]
    public float currentCapacity;
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
        filling.SetFloat("Vector1_B2746C0A", currentCapacity / 100);
        availableCancerPositions.AddRange(cancerPositions);

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!HeartManager.instance.defeat)
        {
            currentCapacity += gainPerSecond * Time.deltaTime;
            if (currentCapacity >= firstCancerSpawnPercentage && firstCancer == null)
            {
                int index = Random.Range(0, availableCancerPositions.Count);
                firstCancer = Instantiate(cancerPrefab, availableCancerPositions[index].position, cancerPrefab.transform.rotation);
                firstCancerSpot = availableCancerPositions[index];
                availableCancerPositions.RemoveAt(index);
            }
            if (currentCapacity >= secondCancerSpawnPercentage && secondCancer == null)
            {
                int index = Random.Range(0, availableCancerPositions.Count);
                secondCancer = Instantiate(cancerPrefab, availableCancerPositions[index].position, cancerPrefab.transform.rotation);
                secondCancerSpot = availableCancerPositions[index];
                availableCancerPositions.RemoveAt(index);
            }
            if (currentCapacity >= thirdCancerSpawnPercentage && thirdCancer == null)
            {
                int index = Random.Range(0, availableCancerPositions.Count);
                thirdCancer = Instantiate(cancerPrefab, availableCancerPositions[index].position, cancerPrefab.transform.rotation);
                thirdCancerSpot = availableCancerPositions[index];
                availableCancerPositions.RemoveAt(index);
            }
            if (currentCapacity >= 100f)
            {
                currentCapacity = 100f;
                HeartManager.instance.TakeDamage(pvLossPerSecond * Time.deltaTime);
                if(!anim.GetBool("Danger"))
                    anim.SetBool("Danger", true);
            }
            else
            {
                if (anim.GetBool("Danger"))
                    anim.SetBool("Danger", false);
            }
            filling.SetFloat("Vector1_B2746C0A", currentCapacity / 100);
            if (currentCapacity != 0f)
                InteractManager.instance.brainButton.SetTrigger("Open");
            else
                InteractManager.instance.brainButton.SetTrigger("Close");
        }
    }

    public void ReduceStress()
    {
        currentCapacity -= stressReductionPerInput;
        if(currentCapacity < firstCancerSpawnPercentage && firstCancer != null)
        {
            firstCancer.GetComponent<Animator>().SetTrigger("Destroy");
            firstCancer = null;
            availableCancerPositions.Add(firstCancerSpot);
            firstCancerSpot = null;
        }
        if (currentCapacity < secondCancerSpawnPercentage && secondCancer != null)
        {
            secondCancer.GetComponent<Animator>().SetTrigger("Destroy");
            secondCancer = null;
            availableCancerPositions.Add(secondCancerSpot);
            secondCancerSpot = null;
        }
        if (currentCapacity < thirdCancerSpawnPercentage && thirdCancer != null)
        {
            thirdCancer.GetComponent<Animator>().SetTrigger("Destroy");
            thirdCancer = null;
            availableCancerPositions.Add(thirdCancerSpot);
            thirdCancerSpot = null;
        }
        if (currentCapacity < 0f)
            currentCapacity = 0f;
        filling.SetFloat("Vector1_B2746C0A", currentCapacity / 100);
    }
}
