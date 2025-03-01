using Time;
using UnityEngine;

public class EnergyTickManager : MonoBehaviour
{
    private float tickRate;

    [SerializeField]
    private string tickRateVariable = "EnergyPerTick";
    [SerializeField]
    private string energyVariable = "Energy";

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

    }
}
