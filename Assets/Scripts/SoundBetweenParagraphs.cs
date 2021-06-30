using UnityEngine;
using UnityEngine.Audio;

public class SoundBetweenParagraphs : MonoBehaviour
{
   [SerializeField] AudioSource source;
   [SerializeField]AudioClip clip;
   [SerializeField] C_Scene sceneManager;

    private void Start()
    {
        sceneManager.INFinishedParagraph += PlaySound;
    }

    public void PlaySound()
    {
        source.clip = clip;
        source.Play();
    }
}
