using UnityEngine;
using TMPro;

public class MultiLanguageText : MonoBehaviour
{
    [SerializeField] string englishText;
    [SerializeField] string portugueseText;
    TextMeshProUGUI text;

    void Start()
    {
        Language.instance.languageChanged += ChangeLanguage;
        text = GetComponent<TextMeshProUGUI>();
        ChangeLanguage();
    }

    void ChangeLanguage()
    {
        if(Language.instance.language == 0)
        {
            text.text = portugueseText;
        }
        else
        {
            text.text = englishText;
        }
    }
}
