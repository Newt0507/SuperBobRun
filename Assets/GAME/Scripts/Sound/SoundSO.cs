using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SoundSO")]
public class SoundSO : ScriptableObject
{
    [System.Serializable]
    public class Sound
    {
        public ESound sound;
        public AudioClip clip;
    }

    public List<Sound> soundList;
}