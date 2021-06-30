using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField] C_Game manager;
    [SerializeField] RectTransform creditTransform;
    [SerializeField] float MaxY = 1000f;
    [SerializeField] float delayTime = 1f;
    [SerializeField] float avaliableTime = 3f;
    Vector3 savedNewVector;

    private void Start()
    {
        savedNewVector = creditTransform.position;
        savedNewVector.y = MaxY;
        PlayCredits();
    }

    private void Update()
    {
        HandleInput();
    }

    public void PlayCredits()
    {
        StartCoroutine(RollCredits());
    }

    public void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            manager.ChangetoInicialMenu();

        }
    }

    IEnumerator RollCredits()
    {
        yield return new WaitForSeconds(delayTime * Time.deltaTime);
        float normalizedTime = 0;
        float startPos = creditTransform.position.y;
        while (normalizedTime < 1)
        {
            normalizedTime += Time.deltaTime / avaliableTime;
            float newPosY = Mathf.Lerp(startPos, MaxY, normalizedTime);
            savedNewVector.y = newPosY;
            creditTransform.position = savedNewVector;
            yield return null;
        }
        manager.ChangetoInicialMenu();
    }
}
