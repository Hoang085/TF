using System.Linq;
using H2910.Common.Singleton;
using UnityEngine;

namespace Sound
{
    public class SoundPalette : ManualSingletonMono<SoundPalette>
    {
        [SerializeField] private AudioPalette dicAudio = new AudioPalette();

        public AudioClip GetAudioClip(AudioClipType typeClip)
        {
            if (dicAudio.ContainsKey(typeClip))
                return dicAudio[typeClip];

            return dicAudio.FirstOrDefault().Value;
        }

        public void PlayButtonClickSound()
        {
            SoundManager.Instance.PlaySound(SoundPalette.Instance.GetAudioClip(AudioClipType.ButtonClick));
        }
    }

    [System.Serializable]
    public class AudioPalette : SerializableDictionary<AudioClipType, AudioClip>
    {
    }

    public enum AudioClipType
    {
        Win,
        Lose,
        ButtonClick,
        CollectCoin,
    }
}