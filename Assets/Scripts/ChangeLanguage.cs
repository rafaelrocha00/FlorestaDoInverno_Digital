using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] int newLanguage;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Change);
    }


    public void Change()
    {
        Language.instance.ChangeLanguage(newLanguage);
    }
  
}
