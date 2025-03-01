using Time;
using UnityEngine;

/// <summary>
/// Overarching Game Manager script. Allows processing of variable changes.
/// </summary>
public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    [SerializeField]
    private VariableLibrary library;

    private static PublicVariableLibrary plibrary;
    public static PublicVariableLibrary Library { get { return plibrary; } }    


    private void Awake()
    {
        instance = this;

        plibrary = new PublicVariableLibrary(library);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float GetVariable(string name)
    {
        return Library.Mappings[name];
    }
    public static void SetVariable(string name, float value) {
        Library.Mappings[name] = value;
    }

    public static void HandleOperation(GameOperation operation)
    {
        foreach(var op in operation.Operations)
        {
            string input = op.inputVar;
            string output = op.outputVar;
            switch (op.op)
            {
                case ValOperation.Operation.Add:
                    
                    SetVariable(output, GetVariable(output)+GetVariable(input));
                    break;
                case ValOperation.Operation.Subtract:
                    SetVariable(output, GetVariable(output) - GetVariable(input));
                    break;
                case ValOperation.Operation.Multiply:
                    SetVariable(output, GetVariable(output) * GetVariable(input));
                    break;
                case ValOperation.Operation.Divide:
                    SetVariable(output, (float)GetVariable(output) / GetVariable(input));
                    break;
                case ValOperation.Operation.Set:
                    SetVariable(output, GetVariable(output));
                    break;

            }
        }
    }
}
