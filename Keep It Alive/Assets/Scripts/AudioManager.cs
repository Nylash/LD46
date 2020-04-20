using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip testClip;
    [Range(0f, 1f)] public float testVolume;

    [Header("ORGANS SOUNDS")]
    public AudioClip breath;
    [Range(0f, 1f)] public float breathVolume;
    public AudioClip burp;
    [Range(0f, 1f)] public float burpVolume;
    public AudioClip cough;
    [Range(0f, 1f)] public float coughVolume;
    public AudioClip eat1;
    [Range(0f, 1f)] public float eat1Volume;
    public AudioClip eat2;
    [Range(0f, 1f)] public float eat2Volume;
    public AudioClip stomachSound;
    [Range(0f, 1f)] public float stomachSoundVolume;
    public AudioClip heart;
    [Range(0f, 1f)] public float heartVolumeSafe;
    [Range(0f, 1f)] public float heartVolumeDanger;
    public AudioClip leverOpen;
    [Range(0f, 1f)] public float leverOpenVolume;
    public AudioClip leverClose;
    [Range(0f, 1f)] public float leverCloseVolume;
    public AudioClip buzz;
    [Range(0f, 1f)] public float buzzVolume;
    public AudioClip buzz2;
    [Range(0f, 1f)] public float buzz2Volume;

    [Header("PLAYER SOUNDS")]
    public AudioClip footStep_Player;
    [Range(0f, 1f)] public float footStepVolume;
    public AudioClip jump;
    [Range(0f, 1f)] public float jumpVolume;

    [Header("MUSIC")]
    public AudioClip music;
    [Range(0f, 1f)] public float musicVolume;

    [Header("SOURCES")]
    public AudioSource foodSource;
    public AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        musicSource.clip = music;
        musicSource.volume = musicVolume;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            foodSource.PlayOneShot(testClip, testVolume);
    }
}
