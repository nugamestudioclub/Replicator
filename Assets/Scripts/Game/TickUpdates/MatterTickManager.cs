using UnityEngine;

public class MatterTickManager : MonoBehaviour
{
    private float tickRate;

    [SerializeField]
    private string tickRateVariable = "MatterPerTick";
    [SerializeField]
    private string matterVariable = "Matter";
    [SerializeField]
    private string matterProduction = "BotMatterProduction";
    [SerializeField]
    private string collectionAllocationVar = "CollectionAllocation";

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

        if (GameManager.GetVariable(energyVariable) > 0)
        {
            float matterProd = GameManager.GetVariable(matterProduction);

            float botsAllocated = GameManager.GetVariable(collectionAllocationVar);
            float productionPerTick = matterProd * botsAllocated;
            GameManager.SetVariable(tickRateVariable, productionPerTick);

            tickRate = GameManager.GetVariable(tickRateVariable) + 0;

            float tickCount = Time.Time.DeltaTime.GetNumberRaw();
            tickRate *= tickCount;

            GameManager.SetVariable(matterVariable, GameManager.GetVariable(matterVariable) + tickRate);

        }
        else
        {
            GameManager.SetVariable(tickRateVariable, 0);
        }

    }
}
