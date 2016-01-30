using UnityEngine;

public static class RDTransformExtensions
{
    public static void AddToParentLocal(this Transform lhs, Transform rhs)
    {
        var pos = lhs.transform.localPosition;
        var rot = lhs.transform.localRotation;
        var scale = lhs.transform.localScale;

        lhs.parent = rhs;

        lhs.transform.localPosition = pos;
        lhs.transform.localRotation = rot;
        lhs.transform.localScale = scale;
    }

    public static void CopyLocal(this Transform lhs, Transform rhs)
    {
        lhs.transform.localPosition = rhs.transform.localPosition;
        lhs.transform.localRotation = rhs.transform.localRotation;
        lhs.transform.localScale = rhs.transform.localScale;
    }
}
