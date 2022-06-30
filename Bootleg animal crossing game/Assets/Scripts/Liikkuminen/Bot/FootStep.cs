using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootStep : MonoBehaviour
{

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void WalkingStep()
    {
        audioSource.Play();
    }
    private void RunningStep()
    {
        audioSource.Play();
    }
}
