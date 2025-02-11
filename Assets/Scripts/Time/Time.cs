using System;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Time
{
    public class Time
    {
        private int[] time;
        private int baseExponent = -3;
        private int maxExponent = 32;

        private static Time deltaTime;
        public static Time DeltaTime { get
            {
                if (deltaTime is null)
                {
                    deltaTime = new Time(UnityEngine.Time.deltaTime);
                }
                deltaTime.SetNumber(UnityEngine.Time.deltaTime);
                return deltaTime;
            } 
        }

        public Time(int baseExponent = -3, int maxExponent = 32) {
            this.time = new int[maxExponent - baseExponent];
            this.baseExponent = baseExponent;
            this.maxExponent = maxExponent;
        }

        public Time(float startingValue, int baseExponent = -3, int maxExponent = 32)
        {
            this.time = new int[maxExponent - baseExponent];
            this.baseExponent = baseExponent;
            this.maxExponent = maxExponent;
            SetNumber(startingValue);
        }

        public void SetNumber(float number)
        {
            // Clear the time array
            Array.Clear(this.time, 0, this.time.Length);

            // Start from the highest power and work down to the smallest
            for (int i = maxExponent - 1; i >= baseExponent; i--)
            {
                // Get the coefficient for this power of 10
                int coefficient = Mathf.FloorToInt(number / Mathf.Pow(10, i));

                // Store the coefficient and remove it from number
                this.time[i - baseExponent] = coefficient;
                number -= coefficient * Mathf.Pow(10, i);
            }
        }


        public float GetNumberRaw()
        {
            float total = 0;
            for (int i = baseExponent; i < maxExponent; i++)
            {
                total += this.time[i - baseExponent] * Mathf.Pow(10, i);
            }
            return total;
        }

        public float GetNumber(int power = 0)
        {
            return this.time[power - baseExponent] * Mathf.Pow(10, power);
        }

        public int GetPrefix(int power = 0)
        {
            return this.time[power - baseExponent];
        }

        private void SetPrefix(int power, int value)
        {
            this.time[power - baseExponent] = value;
        }

        public static Time operator +(Time time1, Time time2)
        {
            int t1Base = time1.baseExponent;
            int t2Base = time2.baseExponent;
            int t1Max = time1.maxExponent;
            int t2Max = time2.maxExponent;
            int _base = Mathf.Min(t1Base, t2Base);
            int _max = Mathf.Max(t1Max, t2Max);

            Time ret = new Time(0, baseExponent: _base, maxExponent: _max);
            int rollOver = 0;

            for (int i = _base; i < _max; i++)
            {
                int prefixTotal = 0;
                if (t1Base <= i && i <= t1Max)
                {
                    prefixTotal += time1.GetPrefix(i);
                }
                if (t2Base <= i && i <= t2Max)
                {
                    prefixTotal += time2.GetPrefix(i);
                }
                prefixTotal += rollOver;
                rollOver = prefixTotal / 10;
                prefixTotal %= 10;
                ret.SetPrefix(i, prefixTotal);
            }

            return ret;
        }

        public static Time operator -(Time time1, Time time2)
        {
            int t1Base = time1.baseExponent;
            int t2Base = time2.baseExponent;
            int t1Max = time1.maxExponent;
            int t2Max = time2.maxExponent;
            int _base = Mathf.Min(t1Base, t2Base);
            int _max = Mathf.Max(t1Max, t2Max);

            Time ret = new Time(0, baseExponent: _base, maxExponent: _max);
            int borrow = 0;

            for (int i = _base; i < _max; i++)
            {
                int prefixTotal = (t1Base <= i && i <= t1Max ? time1.GetPrefix(i) : 0) -
                                  (t2Base <= i && i <= t2Max ? time2.GetPrefix(i) : 0) -
                                  borrow;
                if (prefixTotal < 0)
                {
                    prefixTotal += 10;
                    borrow = 1;
                }
                else
                {
                    borrow = 0;
                }
                ret.SetPrefix(i, prefixTotal);
            }

            return ret;
        }
        public static Time operator *(Time time1, Time time2)
        {
            int _base = time1.baseExponent + time2.baseExponent;
            int _max = time1.maxExponent + time2.maxExponent;
            Time ret = new Time(0, baseExponent: _base, maxExponent: _max);

            for (int i = time1.baseExponent; i < time1.maxExponent; i++)
            {
                for (int j = time2.baseExponent; j < time2.maxExponent; j++)
                {
                    int product = time1.GetPrefix(i) * time2.GetPrefix(j);
                    ret.SetPrefix(i + j, ret.GetPrefix(i + j) + product);
                }
            }

            return ret;
        }

        public static Time operator /(Time time1, Time time2)
        {
            Time ret = new Time();
            ret.SetNumber((float)time1.GetNumberRaw() / (float)time2.GetNumberRaw());
            return ret;
        }


        public static bool operator <(Time time1, Time time2)
        {
            for (int i = time1.maxExponent - 1; i >= time1.baseExponent; i--)
            {
                if (time1.GetPrefix(i) < time2.GetPrefix(i)) return true;
                if (time1.GetPrefix(i) > time2.GetPrefix(i)) return false;
            }
            return false;
        }

        public static bool operator >(Time time1, Time time2) => time2 < time1;
        public static bool operator <=(Time time1, Time time2) => !(time1 > time2);
        public static bool operator >=(Time time1, Time time2) => !(time1 < time2);
        public static bool operator ==(Time time1, Time time2)
        {
            for (int i = time1.maxExponent - 1; i >= time1.baseExponent; i--)
            {
                if (time1.GetPrefix(i) != time2.GetPrefix(i)) return false;
            }
            return true;
        }
        public static bool operator ==(Time t1, object o2)
        {
            return t1.Equals(o2);
        }

        public static bool operator !=(Time time1, Time time2) => !(time1 == time2);

        public static bool operator !=(Time t1, object o2) => !(t1 == o2);

        public override bool Equals(object obj)
        {
            if (obj is Time other)
            {
                return this == other;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            for (int i = baseExponent; i < maxExponent; i++)
            {
                hash = hash * 31 + GetPrefix(i);
            }
            return hash;
        }
    }
}
