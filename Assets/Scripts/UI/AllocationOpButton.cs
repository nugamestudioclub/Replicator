using System.Runtime.InteropServices;
using Time;
using UnityEngine;
using UnityEngine.UI;

public class AllocationOpButton : MonoBehaviour
{
    [SerializeField]
    private string unallocatedVariableName = "Unallocated";
    [SerializeField]
    private string botGroupVariableName = "BotGroupSize";
    [SerializeField]
    private string steamEngineVariableName = "SteamEngines";
    [SerializeField]
    private GameOperation allocationOperation;
    [SerializeField]
    private bool needsEngine;
    private Button btn;

    [SerializeField]
    private bool isAllocating = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(Invoke);
    }
    private void Invoke()
    {
        // If you have no steam engines, you cannot allocate bots to generation
        if (needsEngine && GameManager.GetVariable(steamEngineVariableName) == 0)
        {
            return;
        }
        
        // Check that output is not 0 for decrement
        
        // If you are allocating then check that unallocation is > 0
        if (isAllocating && GameManager.GetVariable(unallocatedVariableName) > 0)
        {
            GameManager.HandleOperation(allocationOperation);
        }
        // If you are deallocating then check that output is > 0
        if(!isAllocating && GameManager.GetVariable(allocationOperation.Operations[0].outputVar)> 0)
        {
            GameManager.HandleOperation(allocationOperation);
        }
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        btn.interactable = isAllocating && GameManager.GetVariable(unallocatedVariableName) > 0 || !isAllocating && GameManager.GetVariable(allocationOperation.Operations[0].outputVar) > 0;
        if (needsEngine && GameManager.GetVariable(steamEngineVariableName) == 0)
        {
            btn.interactable = false;
        }
    }
}
