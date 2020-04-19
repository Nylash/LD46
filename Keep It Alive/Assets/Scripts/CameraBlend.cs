using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBlend : MonoBehaviour
{
    public bool switchCam;
    public GameObject cam1;
    public GameObject cam2;

    CinemachineBrain brain;

    private void Start()
    {
        brain = GetComponent<CinemachineBrain>();
    }
    void Update()
    {
        if (switchCam)
        {
            switchCam = false;
            cam1.SetActive(!cam1.activeSelf);
            cam2.SetActive(!cam2.activeSelf);
        }
    }
}
