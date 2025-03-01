using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Time
{
    public class TimeManager : MonoBehaviour
    {
        public Time CurrentTime { get; private set; }
        public Time CurrentIncrement { get; private set; }

        public static Time Current { get { return TimeManager.instance.CurrentTime; } }

        private static TimeManager instance;

        [Header("Increment Settings")]
        [SerializeField]
        private float startingIncrement = 1f;
        [SerializeField]
        private int incrementExponent = 0;

        private List<Operation> opQueue;



        private void Awake()
        {
            instance = this;
            CurrentTime = new Time(0);
            CurrentIncrement = new Time(startingIncrement,maxExponent: incrementExponent);
            opQueue = new List<Operation>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CurrentTime += CurrentIncrement * Time.DeltaTime;
            List<Operation> rem = new List<Operation>();

            foreach(Operation op in opQueue)
            {
                if (op.isComplete(CurrentTime))
                {
                    rem.Add(op);
                    op.OnComplete.Invoke();
                }
            }
            foreach(Operation op in rem)
            {
                opQueue.Remove(op);
            }
        }

        public void QueueNewOperation(Operation op)
        {
            opQueue.Add(op);
        }
        public static void QueueOperation(Operation op) => instance.QueueNewOperation(op);

    }
}
