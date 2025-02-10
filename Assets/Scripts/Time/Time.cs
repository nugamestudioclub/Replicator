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
                if (deltaTime == null)
                {
                    deltaTime = new Time(UnityEngine.Time.deltaTime);
                }
                deltaTime.SetNumber(UnityEngine.Time.deltaTime);
                return deltaTime;
            } 
        }

        public Time() { }

        public Time(float startingValue, int baseExponent = -3, int maxExponent = 32)
        {
            this.time = new int[maxExponent - baseExponent];
            this.baseExponent = baseExponent;
            this.maxExponent = maxExponent;
            SetNumber(startingValue);
        }

        public void SetNumber(float number)
        {
            for (int i = baseExponent; i < maxExponent; i++)
            {
                float sub = number % Mathf.Pow(10, i);
                sub *= Mathf.Pow(10, -(i + 1));
                int num = Mathf.RoundToInt(sub);
                this.time[i - baseExponent] = num;
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
            if (time2.GetPrefix(time2.baseExponent) == 0)
                throw new DivideByZeroException("Cannot divide by zero");

            int _base = time1.baseExponent - time2.baseExponent;
            int _max = time1.maxExponent - time2.maxExponent;
            Time ret = new Time(0, baseExponent: _base, maxExponent: _max);

            for (int i = time1.maxExponent - 1; i >= time1.baseExponent; i--)
            {
                int dividend = time1.GetPrefix(i);
                int divisor = time2.GetPrefix(time2.baseExponent);
                if (divisor != 0)
                {
                    int quotient = dividend / divisor;
                    ret.SetPrefix(i - time2.baseExponent, quotient);
                }
            }

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

        public static bool operator !=(Time time1, Time time2) => !(time1 == time2);

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
