using UnityEngine;

public class ConstructionModTickManager : MonoBehaviour
{
    [SerializeField]
    private string constructionModVariable = "ConstructionTimeCostMod";
    [SerializeField]
    private string asymptoteVar = "Asymptote";
    [SerializeField]
    private string botsAllocatedVar = "ConstructAllocation";
    [SerializeField]
    private string diminishingReturnThresholdVar = "DiminishedReturnThreshold";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float a = GameManager.GetVariable(asymptoteVar);
        float t = GameManager.GetVariable(diminishingReturnThresholdVar);
        float b = GameManager.GetVariable(botsAllocatedVar);

        float o = (a * b) / (b + t);
        GameManager.SetVariable(constructionModVariable, o);

    }
}
