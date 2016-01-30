using System.Linq;
using UnityEngine;

public static class GameObjectExtensions
{
    public static Bounds GetBounds(this GameObject obj)
    {
        var renderers = obj.GetComponentsInChildren<Renderer>();
        var bounds = new Bounds(renderers.Length > 0 ? renderers.First().bounds.center : obj.transform.position,
            Vector3.zero);
        renderers.ToList().ForEach(r => bounds.Encapsulate(r.bounds));
        obj.GetComponentsInChildren<Collider>().ForEach(
            c =>
                bounds.Encapsulate(new Bounds(obj.transform.TransformPoint(c.bounds.center),
                    obj.transform.TransformPoint(c.bounds.size))));
        return bounds;
    }

    public static Vector3 GetOffsetFromPivot(this GameObject obj)
    {
        var filters = obj.GetComponentsInChildren<MeshFilter>();
        var bounds = new Bounds(filters.Length > 0 ? filters.First().mesh.bounds.center : Vector3.zero, Vector3.zero);
        filters.ToList().ForEach(r => bounds.Encapsulate(r.mesh.bounds));
        return bounds.center;
    }

    public static Bounds GetMeshBounds(this GameObject obj)
    {
        var filters = obj.GetComponentsInChildren<MeshFilter>();
        var bounds = new Bounds(filters.Length > 0 ? filters.First().mesh.bounds.center : Vector3.zero, Vector3.zero);
        filters.ToList().ForEach(r => bounds.Encapsulate(r.mesh.bounds));
        return bounds;
    }
}
