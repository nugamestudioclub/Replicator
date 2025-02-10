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


        private void Awake()
        {
            instance = this;
            CurrentIncrement = new Time(startingIncrement,maxExponent: incrementExponent);
            
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CurrentTime += CurrentIncrement * Time.DeltaTime;
        }

    }
}
