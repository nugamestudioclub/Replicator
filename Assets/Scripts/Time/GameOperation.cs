using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Time
{
    [CreateAssetMenu(fileName = "GameOperation", menuName = "Scriptable Objects/GameOperation")]
    public class GameOperation : ScriptableObject
    {
        [SerializeField]
        private VariableLibrary library;
        [SerializeField]
        private ValOperation[] operations;
    }
}
