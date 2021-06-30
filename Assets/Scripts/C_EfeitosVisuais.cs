using System;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class C_EfeitosVisuais : MonoBehaviour
{
    public Material currentEffect;
    [SerializeField] Material[] effects;
    Action transitionEnded;
    public float baseFadeValue = 2f;

    private void Start()
    {
        FadeOut(baseFadeValue);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(currentEffect != null)
        Graphics.Blit(source, destination, currentEffect);
    }

    public void FadeIn(float Avaliabletime, Action INEndOfTransition = null)
    {
        transitionEnded = INEndOfTransition;
        StartCoroutine(ForwardTransition(Avaliabletime));
    }
  
    public void FadeOut(float Avaliabletime, Action INEndOfTransition = null)
    {
        transitionEnded = INEndOfTransition;
        StartCoroutine(BackwardTransition(Avaliabletime));
    }

    IEnumerator ForwardTransition(float Avaliabletime)
    {
        currentEffect.SetFloat("_Threshold", 0);
        float counter = 0f;
        while(counter < Avaliabletime)
        {
            counter += Time.deltaTime;
            float resultForShader = counter / Avaliabletime;
            currentEffect.SetFloat("_Threshold", resultForShader);
            yield return new WaitForEndOfFrame();
        }
        transitionEnded?.Invoke();
        transitionEnded = null;
    }

    IEnumerator BackwardTransition(float Avaliabletime)
    {
        currentEffect.SetFloat("_Threshold", 1);
        float counter = 0f;
        while (counter < Avaliabletime)
        {
            counter += Time.deltaTime;
            float resultForShader = counter / Avaliabletime;
            currentEffect.SetFloat("_Threshold", 1 - resultForShader);
            yield return new WaitForEndOfFrame();
        }
        transitionEnded?.Invoke();
        transitionEnded = null;
    }
}
