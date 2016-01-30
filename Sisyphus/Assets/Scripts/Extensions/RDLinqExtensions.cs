using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public static class LinqExtensions
{
    public static T SelectRandom<T>(this IEnumerable<T> collection)
    {
        List<T> collectionList = collection.ToList();
        return collectionList[Random.Range(0, collectionList.Count)];
    }

    public static T SelectRandom<T>(this List<T> collectionList)
    {
        return collectionList[Random.Range(0, collectionList.Count)];
    }

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        var forEach = collection as T[] ?? collection.ToArray();
        for (var i = 0; i < forEach.Length; i++)
            action(forEach[i]);

        return forEach;
    }

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T, int> action)
    {
        var forEach = collection as T[] ?? collection.ToArray();
        for (var i = 0; i < forEach.Length; i++)
            action(forEach[i], i);

        return forEach;
    }

    public static T[] Assign<T>(this T[] collection, Func<T, T> func)
    {
        for (var i = 0; i < collection.Length; i++)
            collection[i] = func(collection[i]);

        return collection;
    }

    public static T[] Assign<T>(this T[] collection, Func<T, int, T> func)
    {
        for (var i = 0; i < collection.Length; i++)
            collection[i] = func(collection[i], i);

        return collection;
    }

    public static T[] Clone<T>(this T[] collection)
    {
        var clone = new T[collection.Length];
        return clone.Assign((b, i) => collection[i]);
    }
}
