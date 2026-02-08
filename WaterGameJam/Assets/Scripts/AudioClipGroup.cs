using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AudioClipGroup", menuName = "Scriptable Objects/AudioClipGroup")]
public class AudioClipGroup : ScriptableObject
{
    public List<AudioClip> clips = new List<AudioClip>();
}
