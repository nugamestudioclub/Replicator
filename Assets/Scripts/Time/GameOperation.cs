using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Time
{
    /// <summary>
    /// A list of mathemetical value operations for one in game operation. (ie. Robot Creation)
    /// </summary>
    [CreateAssetMenu(fileName = "GameOperation", menuName = "Scriptable Objects/GameOperation")]
    public class GameOperation : ScriptableObject
    {
        [SerializeField]
        private VariableLibrary library;
        [SerializeField]
        private ValOperation[] operations;

        public ValOperation[] Operations
        {
            get
            {
                return operations;
            }
        }
    }
}
