
public static class RDUnityExtensions
{
    public static T Instantiate<T>(this T prefab) where T : UnityEngine.Object
    {
        return (T)UnityEngine.Object.Instantiate(prefab);
    }
}
