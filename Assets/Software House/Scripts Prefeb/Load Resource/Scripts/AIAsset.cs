using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AIAsset : MonoBehaviour
{
    #region Singleton
    public static AIAsset instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private void Start()
    {
        actionAIAsset = Resources.LoadAll<ActionAI>("AI/Action").ToList();
        stateAIAsset = Resources.LoadAll<StateAI>("AI/State").ToList();
    }

    public List<ActionAI> actionAIAsset;
    public List<StateAI> stateAIAsset;

    public StateAI starterStateAI;

    public ActionAI FindAction(string actionName)
    {
       return actionAIAsset.Find(e => e.ToString() == actionName);
    }

    public StateAI FindState(string stateName)
    {
        return stateAIAsset.Find(e => e.ToString() == stateName);
    }

    public StateAI StarterStateAI()
    {
        return starterStateAI;
    }
}
