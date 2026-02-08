using UnityEngine;
using UnityEngine.Audio;

public class AudioTransitions : MonoBehaviour
{
    public AudioMixerSnapshot underwater;
    public AudioMixerSnapshot outOfWater;
    public float transitionSpeed = 0.5f;
    public void underwaterToOverwater()
    {
        outOfWater.TransitionTo(transitionSpeed);
    }

    public void overwaterToUnderwater()
    {
        underwater.TransitionTo(transitionSpeed);

    }
}
