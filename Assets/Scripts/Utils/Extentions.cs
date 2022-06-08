namespace Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;
    using Random = UnityEngine.Random;

    public static class Extentions
    {
        public static T GetRandom<T>(this IEnumerable<T> list)
        {
            T result;
            int rnd = Random.Range(0, list.Count());
            return list.ElementAt(rnd);
        }
    }

}