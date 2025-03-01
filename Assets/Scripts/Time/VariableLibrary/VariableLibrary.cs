using System.Collections.Generic;
using UnityEngine;

namespace Time
{
    /// <summary>
    /// Initializer Scriptable object for variable storage and initialization.
    /// </summary>
    [CreateAssetMenu(fileName = "VariableLibrary", menuName = "Scriptable Objects/VariableLibrary")]
    public class VariableLibrary : ScriptableObject
    {
        [SerializeField]
        private VarMapping[] mappings;

        // Added method to expose the mappings for initialization
        public VarMapping[] GetMappings()
        {
            return mappings;
        }

        [System.Serializable]
        public class VarMapping // Made public to be accessible from RuntimeVariableLibrary
        {
            public string variableName;
            public float initialValue;
            [SerializeField]
            private string editorDescription;
        }
    }

    /// <summary>
    /// Copy of the VariableLibrary object for read/write on local game instance.
    /// </summary>
    public class PublicVariableLibrary
    {

        private Dictionary<string, float> mappings;
        public Dictionary<string,float> Mappings { get { return mappings; } }   
        public PublicVariableLibrary(VariableLibrary library)
        {
            mappings = new Dictionary<string, float>();
            foreach(var mapping in library.GetMappings())
            {
                mappings[mapping.variableName] = mapping.initialValue + 0;
            }
        }

      
    }
}
