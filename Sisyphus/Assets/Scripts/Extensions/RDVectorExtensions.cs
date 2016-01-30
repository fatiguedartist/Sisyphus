using Sisyphus;
using UnityEngine;

public static class RDVectorExtensions
{
    static Vector3 diff;
    static Vector3 vVector1;
    static Vector3 vVector2;
    static float d;
    static float t;
    static Vector3 vVector3;
    static Vector3 vClosestPoint;

    public static Int3 ToIntVector(this Vector3 vec)
    {
        return new Int3((int)vec.x, (int)vec.y, (int)vec.z);
    }

    public static void SetX(this Transform transform, float x, Space space = Space.Self)
    {
        if (space == Space.Self)
            transform.localPosition = transform.localPosition.ScaleBy(0, 1, 1) + x*Vector3.right;
        else
            transform.position = transform.position.ScaleBy(0, 1, 1) + x*Vector3.right;
    }

    public static void SetY(this Transform transform, float y, Space space = Space.Self)
    {
        if (space == Space.Self)
            transform.localPosition = transform.localPosition.ScaleBy(1, 0, 1) + y*Vector3.up;
        else
            transform.position = transform.position.ScaleBy(1, 0, 1) + y*Vector3.up;
    }

    public static void SetZ(this Transform transform, float z, Space space = Space.Self)
    {
        if (space == Space.Self)
            transform.localPosition = transform.localPosition.ScaleBy(1, 1, 0) + z*Vector3.forward;
        else
            transform.position = transform.position.ScaleBy(1, 1, 0) + z*Vector3.forward;
    }

    public static Vector2 Inverse(this Vector2 lhs, Vector2 rhs)
    {
        return new Vector2(lhs.x/rhs.x, lhs.y/rhs.y);
    }

    public static Vector3 Inverse(this Vector3 lhs, Vector3 rhs)
    {
        return new Vector3(lhs.x/rhs.x, lhs.y/rhs.y, lhs.z/rhs.z);
    }

    public static Vector3 Inverse(this Vector3 lhs, float x, float y, float z)
    {
        return new Vector3(lhs.x/x, lhs.y/y, lhs.z/z);
    }

    public static Vector2 ScaleBy(this Vector2 lhs, Vector2 rhs)
    {
        return new Vector2(lhs.x*rhs.x, lhs.y*rhs.y);
    }

    public static Vector3 ScaleBy(this Vector3 lhs, Vector3 rhs)
    {
        return new Vector3(lhs.x*rhs.x, lhs.y*rhs.y, lhs.z*rhs.z);
    }

    public static Vector3 ScaleBy(this Vector3 lhs, float x, float y, float z)
    {
        return new Vector3(lhs.x*x, lhs.y*y, lhs.z*z);
    }

    public static Vector3 Abs(this Vector3 vec)
    {
        return new Vector3(Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z));
    }

    public static Vector3 Min(this Vector3 lhs, Vector3 rhs)
    {
        return Vector3.Min(lhs, rhs);
    }

    public static Vector3 Max(this Vector3 lhs, Vector3 rhs)
    {
        return Vector3.Max(lhs, rhs);
    }

    public static Vector2 Mod(this Vector2 vec, float val)
    {
        return new Vector2(vec.x%val, vec.y%val);
    }

    public static float Largest(this Vector3 vec)
    {
        return Mathf.Max(vec.x, vec.y, vec.z);
    }

    public static float Smallest(this Vector3 vec)
    {
        return Mathf.Min(vec.x, vec.y, vec.z);
    }

    public static Vector3 Clamp(this Vector3 vPoint, Vector3 vA, Vector3 vB)
    {
        return new Vector3(
            Mathf.Clamp(vPoint.x, vA.x, vB.x),
            Mathf.Clamp(vPoint.y, vA.y, vB.y),
            Mathf.Clamp(vPoint.z, vA.z, vB.z)
            );
    }

    public static Vector3 ClosestPointOnLine(this Vector3 vPoint, Vector3 vA, Vector3 vB)
    {
        diff = (vB - vA);
        vVector1 = vPoint - vA;
        vVector2 = diff.normalized;

        d = diff.sqrMagnitude;
        t = Vector3.Dot(vVector2, vVector1);

        if (t <= 0)
        {
            return vA;
        }

        if (t*t >= d)
        {
            return vB;
        }

        vVector3 = vVector2*t;
        vClosestPoint = vA + vVector3;
        return vClosestPoint;
    }
}
