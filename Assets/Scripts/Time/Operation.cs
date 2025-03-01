using System.Data.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Time
{
    public class Operation
    {
        private Time startTime;
        private Time endTime;
        private UnityEvent onComplete;
        public UnityEvent OnComplete { get { return onComplete; } }
        private string id;

        private Time totalDur;

        public string Id { get { return id; } }
        

        public Operation(string id, Time startTime,  Time endTime)
        {
            this.id = id;
            this.startTime = startTime;
            this.endTime = endTime;
            this.onComplete = new UnityEvent();
            totalDur = endTime - startTime;
        }
        public Operation(string id, Time startTime, Time endTime, UnityEvent onComplete)
        {
            this.id = id;
            this.startTime = startTime;
            this.endTime = endTime;
            this.onComplete = onComplete;
            totalDur = endTime - startTime;
        }

        public float GetProgress(Time cur)
        {
            
            Time prog = cur - startTime;
            prog = prog/totalDur;
            


            return prog.GetNumberRaw();
        }

        public bool isComplete(Time cur)
        {
            return cur >= endTime;
        }

    }
}
