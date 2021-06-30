using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class C_Scene : MonoBehaviour
{
    Scene currentScene = null;
    [SerializeField] float timeBetweenTextParagraph = 1f;
    [SerializeField]List<SceneChanger> buttons;
    [SerializeField] GameObject grid;
    [SerializeField] GameObject textTemplate;
    GameObject currentillustration;
    [SerializeField] List<Scene> scenes;
    [SerializeField] List<GameObject> illustrations;

    Action INFinishedAnimatingText;
    public Action INFinishedParagraph;
    public Action INFinishedChangingScene;

    [SerializeField] C_Game manager;

    private void Start()
    {
        UpdateScene(0);
    }

    private void Update()
    {
        HandleInput();
    }

    public void UpdateScene(int newSceneID)
    {
        ResetButtons();
        Scene scene = scenes.Find(x => x.GetSceneID() == newSceneID);
        if (scene.changestate)
        {
            scene.ChangeStates();
        }
        currentScene = scene;
        ClearDescriptionBox();

        UpdateIllustration(scene);
        INFinishedAnimatingText += FillAllButtons;
        UpdateSceneDescription(scene);

        INFinishedChangingScene?.Invoke();
    }

    public void UpdateSceneDescription(Scene scene)
    {
        string[] splitedText = scene.GetDescription().Split('\n');
        StartCoroutine(AnimateText(splitedText));
    }

    public void ClearDescriptionBox()
    {
        for (int i = 0; i < grid.transform.childCount; i++)
        {
           Destroy(grid.transform.GetChild(i).gameObject);
        }
    }

    public void ShowEndGameButton()
    {
        buttons[0].gameObject.SetActive(true);
        buttons[0].name.text = "->";
        buttons[0].ChangeButtonAction(delegate { manager.ChangeUnityScene(2); });
    }


    IEnumerator AnimateText(string[] text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            TextMeshProUGUI Ntext = Instantiate(textTemplate, grid.transform).GetComponent<TextMeshProUGUI>();
            Ntext.color = Color.clear;
            Ntext.text = text[i];
            Ntext.text += '\n';

            float alpha = 0;
            float counter = 0f;
            Color newcolor = Color.white;

            while (counter < timeBetweenTextParagraph)
            {
                counter += Time.deltaTime;
                alpha += counter / timeBetweenTextParagraph;
                newcolor.a = alpha;
                ChangeTextMeshColor(Ntext, newcolor);
                yield return new WaitForSeconds(Time.deltaTime);
            }

            INFinishedParagraph?.Invoke();
        }

        INFinishedAnimatingText?.Invoke();
        INFinishedAnimatingText = null;
    }

    public void ChangeTextMeshColor(TextMeshProUGUI texto, Color newColor)
    {
        texto.color = newColor;
    }

    public void ChangeTextMeshVertexColor(Color newColor, TextMeshProUGUI destination, int firstCharToStart)
    {
        destination.ForceMeshUpdate();

        TMP_TextInfo textInfo = destination.textInfo;
        int currentCharacter = firstCharToStart;
        Color32[] CharacterColors;

        for (int i = currentCharacter; i < textInfo.characterCount; i++)
        {
            Debug.Log(currentCharacter + " de " + textInfo.characterCount);
            int materialIndexOfCurrentChar = textInfo.characterInfo[i].materialReferenceIndex;
            int vertexIndexOfCurrentChar = textInfo.characterInfo[i].vertexIndex;
            CharacterColors = textInfo.meshInfo[materialIndexOfCurrentChar].colors32;

            if (textInfo.characterInfo[i].isVisible)
            {
                CharacterColors[vertexIndexOfCurrentChar + 0] = newColor;
                CharacterColors[vertexIndexOfCurrentChar + 1] = newColor;
                CharacterColors[vertexIndexOfCurrentChar + 2] = newColor;
                CharacterColors[vertexIndexOfCurrentChar + 3] = newColor;
                destination.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            }

        }
    
    }

    public void UpdateIllustration(Scene scene)
    {
        if (currentillustration != null)
        {
            currentillustration.SetActive(false);
        }
        currentillustration = illustrations[scene.GetIllustrationID()];
        currentillustration.SetActive(true);
    }

    public void FillAllButtons()
    {
        Scene scene = currentScene;
        int buttonsUsed = 0;
        float choiceCount = scene.GetNumberOfChoices();
        if(choiceCount == 0)
        {
            ShowEndGameButton();
        }
        for (int i = 0; i < choiceCount; i++)
        {
            int id = scene.GetChoice(i).newScene;
            Scene sceneAssociate = scenes.Find(x => x.GetSceneID() == id);
            if (sceneAssociate.CanAppear())
            {
                FillButton(buttonsUsed, scene.GetChoice(i));
                buttonsUsed++;
            }
        }
    }

    public void FillButton(int buttonIndex, Choice choice)
    {
        buttons[buttonIndex].gameObject.SetActive(true);
        if(Language.instance.language == 0)
        {
            buttons[buttonIndex].name.text = choice.name;
        }
        else
        {
            buttons[buttonIndex].name.text = choice.englishName;

        }
        buttons[buttonIndex].newSceneID = choice.newScene;
    }

    public void ResetButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            manager.ChangetoInicialMenu();
        }
    }



}
