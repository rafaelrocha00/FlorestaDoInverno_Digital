using System;
using UnityEngine;

public class Language : MonoBehaviour
{
    public Action languageChanged;
    public int language { get; private set; }
    public static Language instance;

    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ChangeLanguage(int language)
    {
        this.language = language;
        languageChanged?.Invoke();
    }
}
