using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ
{
    public class NumberTools
    {
        public static float TrimFloat(float f, int metric_like_100)
        {
            return ((float)((int)(f * metric_like_100))) / metric_like_100;
        }

        public static float TrimFloat10000(float f)
        {
            return ((float)((int)(f * 10000))) / 10000;
        }

        public static float TrimFloat1000(float f)
        {
            return ((float)((int)(f * 1000))) / 1000;
        }

        public static float TrimFloat100(float f)
        {
            return ((float)((int)(f * 100))) / 100;
        }

        public static float TrimFloat10(float f)
        {
            return ((float)((int)(f * 10))) / 10;
        }

        /// <summary>
        /// int to bool:
        /// int<=0 is false
        /// int>0 is true  
        /// </summary>
        public static bool IntToBool(int i)
        {
            return i > 0 ? true : false;
        }

        /// <summary>
        /// int[] to string like [1,2,3,4,5]: 
        /// </summary>
        public static string ArrayToString(int[] array, string separator = ",")
        {
            return "[" + ArrayToStringNoBrackets(array, separator) + "]";
        }

        /// <summary>
        /// int[] to string like 1,2,3,4,5: 
        /// </summary>
        public static string ArrayToStringNoBrackets(int[] array, string separator = ",")
        {
            string s = "";
            for (int i = 0; i < array.Length; i++)
            {
                s += array[i] + separator;
            }
            if (s.Length > 2) s = s.Remove(s.Length - separator.Length, separator.Length); //Убрать последнюю запятую
            return s;
        }

        /// <summary>
        ///  long to int[] like 12345 to [1,2,3,4,5]
        /// </summary>
        public static int[] NumberToIntArray(long number, int n = 1)
        {
            List<int> nums = new List<int>();
            int r = (int)Math.Pow(10, n);

            if (n == 0)
            {
                nums.Add(0);
            }
            else
            {
                while (number > 0)
                {
                    nums.Insert(0, (int)(number % r));
                    number = number / r;
                }
            }
            return nums.ToArray();
        }

        /// <summary>
        ///  long to byte[] like 12345 to [1,2,3,4,5]
        /// </summary>
        public static byte[] NumberToByteArray(long number, int n = 1)
        {
            List<byte> nums = new List<byte>();
            int r = (int)Math.Pow(10, n);

            if (n == 0)
            {
                nums.Add(0);
            }
            else
            {
                while (number > 0)
                {
                    nums.Insert(0, (byte)(number % r));
                    number = number / r;
                }
            }
            return nums.ToArray();
        }

        /// <summary>
        ///  long to sbyte[] like 12345 to [1,2,3,4,5]
        /// </summary>
        public static sbyte[] NumberToSbyteArray(long number, int n = 1)
        {
            List<sbyte> nums = new List<sbyte>();
            int r = (int)Math.Pow(10, n);

            if (n == 0)
            {
                nums.Add(0);
            }
            else
            {
                while (number > 0)
                {
                    nums.Insert(0, (sbyte)(number % r));
                    number = number / r;
                }
            }
            return nums.ToArray();
        }
    }
}