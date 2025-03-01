using UnityEngine;

namespace Time
{
    /// <summary>
    /// A single Value Operation 
    /// </summary>
    [System.Serializable]
    public class ValOperation
    {
        public enum Operation { Add, Subtract, Multiply, Divide, Set };

        public Operation op;
        public string inputVar;
        public string outputVar;

        public ValOperation() {
            this.op = Operation.Add;
            inputVar = string.Empty;
            outputVar = string.Empty;
        }
    }
}
