using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public static class ComponentExtensions
{
    public static T InstantiateToParent<T>(this T obj, Transform parent) where T : Component
    {
        var ret = (T) Object.Instantiate(obj);
        ret.transform.parent = parent;
        return ret;
    }

    public static GameObject InstantiateToParentLocal(this GameObject obj, Transform parent)
    {
        var ret = Object.Instantiate(obj);
        var pos = ret.transform.localPosition;
        var rot = ret.transform.localRotation;
        var scale = ret.transform.localScale;

        ret.transform.parent = parent;

        ret.transform.localPosition = pos;
        ret.transform.localRotation = rot;
        ret.transform.localScale = scale;

        return ret;
    }

    public static T InstantiateToParentLocal<T>(this T obj, Transform parent) where T : Component
    {
        var ret = (T) Object.Instantiate(obj);
        var pos = ret.transform.localPosition;
        var rot = ret.transform.localRotation;
        var scale = ret.transform.localScale;

        ret.transform.parent = parent;

        ret.transform.localPosition = pos;
        ret.transform.localRotation = rot;
        ret.transform.localScale = scale;

        return ret;
    }

    public static T Find<T>(this Object obj) where T : Object
    {
        return (T) Object.FindObjectOfType(typeof (T));
    }

    public static IEnumerable<T> FindMany<T>(this Object obj) where T : Object
    {
        return (IEnumerable<T>) Object.FindObjectsOfType(typeof (T));
    }

    public static void Invoke(this MonoBehaviour obj, Action action, float inSeconds, Func<bool> predicate = null)
    {
        obj.StartCoroutine(InvokeInSeconds(action, inSeconds, predicate));
    }

    static IEnumerator InvokeInSeconds(Action action, float seconds, Func<bool> predicate)
    {
        yield return new WaitForSeconds(seconds);
        if (predicate == null || predicate())
            action();
    }
}
