using System.Collections.Generic;
using UnityEngine;

namespace Time
{
    [CreateAssetMenu(fileName = "VariableLibrary", menuName = "Scriptable Objects/VariableLibrary")]
    public class VariableLibrary : ScriptableObject
    {

        [SerializeField]
        private VarMapping[] mappings;
        private Dictionary<string, float> _mappings;
        private Dictionary<string, float> _pMappings;

        public Dictionary<string, float> Mappings
        {
            get
            {
                if (_mappings == null|| (_pMappings == null) || (_pMappings!=_mappings))
                {
                    _mappings = new Dictionary<string, float>();
                    foreach (var mapping in mappings)
                    {
                        _mappings[mapping.variableName] = mapping.initialValue;
                    }
                    _pMappings = _mappings;
                }
                return _mappings;
            }
        }

        [System.Serializable]
        class VarMapping
        {
            public string variableName;
            public float initialValue;
        }
    }

}


