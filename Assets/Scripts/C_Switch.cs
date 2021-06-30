using System.Linq;
using UnityEngine;

public class C_Switch : MonoBehaviour
{
    public static C_Switch instancia;
    [SerializeField] Switch[] possibilidades;

    private void Awake()
    {
        if(instancia != null && instancia != this)
        {
            Destroy(this);
        }
        else
        {
            instancia = this;
        }
    }

    public void ChangeCondition(int id, bool newState)
    {
        possibilidades[id].state = newState;
    }

    public void ChangeCondition(string name, bool newState)
    {
        possibilidades.FirstOrDefault(x => x.name == name).state = newState; 
    }

    public bool GetConditionState(int id)
    {
        if(possibilidades.Length <= 0)
        {
            Debug.Log("switch vazio.");
        }
        return possibilidades[id].state;
    }

    public bool GetConditionState(string name)
    {
        return possibilidades.FirstOrDefault(x => x.name == name).state;
    }
}

[System.Serializable]
public class Switch
{
    public string name;
    public bool state;
}

[System.Serializable]
public class Switch_Id
{
    public int id;
    public bool state;
}