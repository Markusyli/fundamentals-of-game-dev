using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] stepAudioClips;
    public AudioClip[] hitAudioClips;
    public AudioSource stepAudioSource;
    public AudioSource hitAudioSource;

    private void Step()
    {
        PlayAudio(stepAudioClips, stepAudioSource);
    }

    private void Hit()
    {
        PlayAudio(hitAudioClips, hitAudioSource);
    }

    private void PlayAudio(AudioClip[] clipArray, AudioSource audioSource)
    {
        AudioClip audioClip = clipArray[Random.Range(0, clipArray.Length)];
        audioSource.PlayOneShot(audioClip);
    }
}
