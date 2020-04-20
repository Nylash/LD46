using UnityEngine;

public class EndInteract : MonoBehaviour
{
    public AudioSource walkSource;
    public AudioSource jumpLandingSource;

    public void EndInteraction()
    {
        PlayerManager.instance.animController.SetBool("Interacting", false);
    }

    public void WalkSound()
    {
        walkSource.PlayOneShot(AudioManager.instance.footStep_Player, AudioManager.instance.footStepVolume);
    }

    public void JumpLandingEffect()
    {
        jumpLandingSource.PlayOneShot(AudioManager.instance.jump, AudioManager.instance.jumpVolume);
        //jump/land FX
    }
}
