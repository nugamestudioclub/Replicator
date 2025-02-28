using System.Collections.Generic;
using UnityEngine;

namespace Time
{
    public class RuntimeVariableLibrary : MonoBehaviour
    {
        private Dictionary<string, float> variableValues = new Dictionary<string, float>();

        // This method initializes the RuntimeVariableLibrary with values from a given VariableLibrary
        public void Initialize(VariableLibrary variableLibrary)
        {
            foreach (VariableLibrary.VarMapping mapping in variableLibrary.GetMappings())
            {
                variableValues[mapping.variableName] = mapping.initialValue;
            }
        }

        // Method to get a variable value
        public float GetVariable(string name)
        {
            if (variableValues.ContainsKey(name))
            {
                return variableValues[name];
            }
            Debug.LogError("Variable not found: " + name);
            return 0; // Return default value or throw an exception based on your preference
        }

        // Method to set a variable value
        public void SetVariable(string name, float value)
        {
            if (variableValues.ContainsKey(name))
            {
                variableValues[name] = value;
            }
            else
            {
                Debug.LogError("Variable not found: " + name);
            }
        }
    }


}
