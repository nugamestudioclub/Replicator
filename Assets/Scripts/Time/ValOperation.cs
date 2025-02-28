using UnityEngine;

namespace Time
{
    [System.Serializable]
    public class ValOperation
    {
        public enum Operation { Add, Subtract, Multiply, Divide};

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
