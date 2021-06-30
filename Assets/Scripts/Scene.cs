using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NovaCena", menuName = "Cena")]
public class Scene : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] int illustration;
    [SerializeField]List<Switch_Id> requirements;
    public bool changestate = false;
    [SerializeField][TextArea(0, 30)] string DescricaoDeCena;
    [SerializeField] [TextArea(0, 30)] string DescricaoDeCenaIngles;
    [SerializeField] List<Choice> choices;

    public int GetSceneID()
    {
        return id;
    }

    public void ChangeStates()
    {
        if (requirements.Count > 0 && changestate)
        {
            for (int i = 0; i < requirements.Count; i++)
            {
                C_Switch.instancia.ChangeCondition(requirements[i].id, requirements[i].state);
            }
        }
    }

    public int GetIllustrationID()
    {
        return illustration;
    }

    public string GetDescription()
    {
        if(Language.instance.language == 1)
        {
            return DescricaoDeCenaIngles;
        }
        return DescricaoDeCena;
    }

    public int GetNumberOfChoices()
    {
        return choices.Count;
    }

    public Choice GetChoice(int i)
    {
        if(i >= choices.Count)
        {
            Debug.LogWarning("Não há a escolha de id " + i);
        }
        return choices[i];
    }

    public bool CanAppear()
    {
        if (requirements.Count > 0 && !changestate)
        {
            Debug.Log("Condição observada em " + name);
            bool result = false;
            for (int i = 0; i < requirements.Count; i++)
            {
              result = (C_Switch.instancia.GetConditionState(requirements[i].id) == requirements[i].state);
                Debug.Log("Requisito " + requirements[i].id + " é " + result);
              if (result == false) return false;
            }
        }

        return true;
    }

}
