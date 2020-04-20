using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeScreenManager : MonoBehaviour
{
    public static ShakeScreenManager instance;

    [Header("CONFIGURATION")]
    public float shakeDuration = .5f;
    public float shakeAmplitude = 1f;
    public float shakeFrequency = 1f;

    [Header("COMPONENTS")]
    public GameObject cam1;
    public GameObject cam2;
    public CinemachineBasicMultiChannelPerlin cam1noise;
    public CinemachineBasicMultiChannelPerlin cam2noise;

    [Header("VARIABLES")]
    Coroutine shakeCoroutine;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        cam1noise = cam1.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cam2noise = cam2.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeScreen()
    {
        if (shakeCoroutine == null)
            shakeCoroutine = StartCoroutine(DoShakeScreen());
    }

    IEnumerator DoShakeScreen()
    {
        if (cam1.activeSelf)
        {
            cam1noise.m_AmplitudeGain = shakeAmplitude;
            cam1noise.m_FrequencyGain = shakeFrequency;
            yield return new WaitForSeconds(shakeDuration);
            cam1noise.m_AmplitudeGain = 0;
        }
        else
        {
            cam2noise.m_AmplitudeGain = shakeAmplitude;
            cam2noise.m_FrequencyGain = shakeFrequency;
            yield return new WaitForSeconds(shakeDuration);
            cam2noise.m_AmplitudeGain = 0;
        }
        yield return new WaitForSeconds(shakeDuration);
        shakeCoroutine = null;
    }
}
