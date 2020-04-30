// RZ.Singleton 1.0
//
// Пример одноэкземплярного класса.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ
{
    public class Singleton
    {

        protected static Singleton instance;
        public Singleton()
        {
        }

        static void init()
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
        }

        public static Singleton Instance
        {
            get
            {
                init();
                return instance;
            }

        }
    }
}