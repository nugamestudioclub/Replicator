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
    private string energyAllocatedVar = "ChargeAllocation";
    [SerializeField]
    private string energyPerTickModVar = "EnergyPerTickMod";

    [SerializeField]
    private string botEnergyProdution = "BotEnergyProduction";

    [SerializeField]
    private string botEnergyUpkeep = "BotEnergyUpkeep";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tickRate = GameManager.GetVariable(tickRateVariable);
    }

    // Update is called once per frame
    void Update()
    {
        // Energy Per Tick Calculation
        float bots = GameManager.GetVariable(botsVar);
        float unallocatedBots = GameManager.GetVariable(unallocatedVar);
        float energyAllocatedBots = GameManager.GetVariable(energyAllocatedVar);
        float botUpkeep = GameManager.GetVariable(botEnergyUpkeep);
        float botProduction = GameManager.GetVariable(botEnergyProdution);

        float energyPerTick = -((bots - unallocatedBots)*botUpkeep) + (energyAllocatedBots*botProduction);
        GameManager.SetVariable(tickRateVariable, energyPerTick);

        // Energy Calculation
        tickRate = GameManager.GetVariable(tickRateVariable) + 0;

        float tickCount = Time.Time.DeltaTime.GetNumberRaw();
        tickRate *= tickCount;

        GameManager.SetVariable(energyVariable, Mathf.Max(0,GameManager.GetVariable(energyVariable) + tickRate));

    }
}
