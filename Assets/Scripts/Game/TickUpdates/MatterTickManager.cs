using UnityEngine;

public class MatterTickManager : MonoBehaviour
{
    private float tickRate;

    [SerializeField]
    private string tickRateVariable = "MatterPerTick";
    [SerializeField]
    private string matterVariable = "Matter";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tickRate = GameManager.GetVariable(tickRateVariable);
    }

    // Update is called once per frame
    void Update()
    {
        tickRate = GameManager.GetVariable(tickRateVariable) + 0;

        float tickCount = Time.Time.DeltaTime.GetNumberRaw();
        tickRate *= tickCount;

        GameManager.SetVariable(matterVariable, GameManager.GetVariable(matterVariable) + tickRate);

    }
}
