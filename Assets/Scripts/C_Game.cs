using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class C_Game : MonoBehaviour
{

    public void ChangetoInicialMenu()
    {
        ChangeUnityScene(0);
    }

    public void ChangeUnityScene(int sceneId)
    {
        C_EfeitosVisuais efeitos = Camera.main.GetComponent<C_EfeitosVisuais>();
        efeitos.FadeIn(efeitos.baseFadeValue);
        StartCoroutine(LoadScene(sceneId , efeitos.baseFadeValue));
    }

    IEnumerator LoadScene(int sceneId, float timeToWaitBeforeLoading = 0)
    {
        yield return new WaitForSeconds(timeToWaitBeforeLoading);
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneId);
        while (!load.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
