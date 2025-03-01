using System.Collections.Generic;
using UnityEngine;

namespace Time
{
    /// <summary>
    /// Initializer Scriptable Object for variable storage and initialization.
    /// </summary>
    [CreateAssetMenu(fileName = "VariableLibrary", menuName = "Scriptable Objects/VariableLibrary")]
    public class VariableLibrary : ScriptableObject
    {
        [SerializeField]
        private VarGroup[] groups;

        /// <summary>
        /// Returns all variable mappings across all groups.
        /// </summary>
        public VarMapping[] GetMappings()
        {
            List<VarMapping> allMappings = new List<VarMapping>();
            foreach (var group in groups)
            {
                if (group.variables != null)
                {
                    allMappings.AddRange(group.variables);
                }
            }
            return allMappings.ToArray();
        }

        public VarGroup[] GetGroups()
        {
            return groups;
        }

        [System.Serializable]
        public class VarGroup
        {
            public string groupName = "New Group";
            public VarMapping[] variables;
        }

        [System.Serializable]
        public class VarMapping
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
        public Dictionary<string, float> Mappings { get { return mappings; } }

        public PublicVariableLibrary(VariableLibrary library)
        {
            mappings = new Dictionary<string, float>();

            foreach (var mapping in library.GetMappings())
            {
                mappings[mapping.variableName] = mapping.initialValue;
            }
        }
    }
}
