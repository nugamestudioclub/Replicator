using System;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Time
{
    public class Time
    {
        private double[] curTime;
        private int basePower;

        /// <summary>
        /// The Time variable for tracking time over high quantities.
        /// </summary>
        /// <param name="minGranularity">The minimum power of 10 in seconds.</param>
        /// <param name="maxGranularity">The maximum power of 10 in seconds.</param>
        public Time(int minGranularity, int maxGranularity)
        {
            int granularity = maxGranularity - minGranularity;
            this.curTime = new double[granularity];
            this.basePower = minGranularity;
        }


        public static bool operator <(Time l, Time r)
        {
            int index = l.curTime.Length - 1;

            while(index >= 0)
            {
                double tl = l.curTime[index];
                double tr = r.curTime[index];

                if(tl < tr)
                {
                    return true;
                }
                index -= 1;
            }
            return false;

        }
        public static bool operator >(Time l, Time r)
        {
            int index = l.curTime.Length - 1;

            while (index >= 0)
            {
                double tl = l.curTime[index];
                double tr = r.curTime[index];

                if (tl > tr)
                {
                    return true;
                }
                index -= 1;
            }
            return false;
        }

        public static bool operator ==(Time l, Time r)
        {
            bool total = true;
            for (int i = 0; i < l.curTime.Length; i++)
            {
                total = ((float)l.curTime[i]) == ((float)r.curTime[i]);
                
            }
            return total;
        }

        public static bool operator !=(Time l, Time r)
        {
            for(int i =0; i < l.curTime.Length; i++)
            {
                if (l.curTime[i] != r.curTime[i])
                {
                    return true;
                }
            }
            return false;
        }

        public static bool operator <=(Time l, Time r)
        {
            return l == r || l < r;
        }
        public static bool operator >=(Time l, Time r)
        {
            return l == r || l > r;
        }

        public static bool Equals(Time l, Time r)
        {
            if (ReferenceEquals(l, r))
                return true;
            if (l is null || r is null)
                return false;
            return l.curTime.SequenceEqual(r.curTime);
        }

        public override int GetHashCode()
        {
            return this.basePower.GetHashCode() ^ this.curTime[0].GetHashCode();
        }

    }
}
