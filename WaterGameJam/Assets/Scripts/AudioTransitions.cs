using UnityEngine;
using UnityEngine.Audio;

public class AudioTransitions : MonoBehaviour
{
    public AudioMixerSnapshot underwater;
    public AudioMixerSnapshot outOfWater;
    public void underwaterToOverwater()
    {
        outOfWater.TransitionTo(1.0f);
    }

    public void overwaterToUnderwater()
    {
        underwater.TransitionTo(1.0f);

    }
}
