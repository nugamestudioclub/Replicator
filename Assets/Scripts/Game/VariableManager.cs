using System.Collections.Generic;
using System.Linq;
using Time;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    private static VariableManager instance;

    [SerializeField]
    private VariableLibrary variables;

    private Dictionary<string, float> mapping;

    private void Awake()
    {
        instance = this;
        Time.VariableLibrary.VarMapping[] vars = variables.GetMappings();
        mapping = new Dictionary<string, float>();
        foreach(var var in vars)
        {
            mapping[var.variableName] = var.initialValue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string[] _GetVariables()
    {
        return mapping.Keys.ToArray();
    }

    private float _GetValue(string variable)
    {
        if (mapping.ContainsKey(variable)) {
            return mapping[variable];
        }
        return -1f;
    }

    private int _SetValue(string variable, float value)
    {
        if (mapping.ContainsKey(variable))
        {
            mapping[variable] = value;
            return 1;
        }
        return 0;
    }

    public static string[] GetVariables()
    {
        return instance._GetVariables();
    }

    public static float GetValue(string variable)
    {
        return instance._GetValue(variable);
    }

    public static int SetValue(string variable, float value)
    {
        return instance._SetValue(variable, value);
    }
}
