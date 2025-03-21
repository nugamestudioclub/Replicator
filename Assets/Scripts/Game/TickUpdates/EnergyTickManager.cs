using NUnit.Framework.Constraints;
using Time;
using UnityEngine;

public class EnergyTickManager : MonoBehaviour
{
    private float tickRate;

    [SerializeField]
    private string tickRateVariable = "EnergyPerTick";
    [SerializeField]
    private string energyVariable = "Energy";

    [SerializeField]
    private string botsVar = "Bots";
    [SerializeField]
    private string unallocatedVar = "Unallocated";
    [SerializeField]
    private string energyPerTickModVar = "EnergyPerTickMod";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tickRate = GameManager.GetVariable(tickRateVariable);
    }

    // Update is called once per frame
    void Update()
    {
        tickRate = GameManager.GetVariable(tickRateVariable)+0;

        float tickCount = Time.Time.DeltaTime.GetNumberRaw();
        tickRate *= tickCount;

        GameManager.SetVariable(energyVariable, GameManager.GetVariable(energyVariable)+tickRate);

        float bots = GameManager.GetVariable(botsVar);
        float unallocatedBots = GameManager.GetVariable(unallocatedVar);
        float energyPerTickMod = GameManager.GetVariable(energyPerTickModVar);
        float energyPerTick = energyPerTickMod - (bots - unallocatedBots);
        GameManager.SetVariable(tickRateVariable, energyPerTick);

    }
}
