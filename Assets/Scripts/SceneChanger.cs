using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SceneChanger : MonoBehaviour
{
    Button button;
    public C_Scene sceneController;
    public TextMeshProUGUI name;
    public int newSceneID;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ClickButton);
    }

    public void ChangeButtonAction(UnityAction newAction)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(newAction);
    }

    public void ClickButton()
    {
        sceneController.UpdateScene(newSceneID);
    }
}
