using UnityEngine;
using UnityEngine.Audio;

public class Button_Sound : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField]AudioClip onClickSound;
    [SerializeField]AudioClip onMouseAboveSound;

    public void PlaySound(AudioClip clip)
    {
        if (clip)
        {
            source.clip = clip;
            source.Play();
        }
    }

    public void PlayMouseAboveSound()
    {
        PlaySound(onMouseAboveSound);
    }

    public void PlayClickSound()
    {
        PlaySound(onClickSound);
    }
}
