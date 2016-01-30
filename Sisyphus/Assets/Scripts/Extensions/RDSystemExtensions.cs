using System;
using System.Linq;

public class EventArgs<T> : EventArgs
{
    public T Value { get; private set; }

    public EventArgs(T val)
    {
        Value = val;
    }
}

public static class EventExtensions
{
    public static void Raise(this Action handler)
    {
        var test = handler;
        if (test == null)
        {
            return;
        }
        test.Invoke();
    }

    public static void Raise<T>(this Action<T> handler, T val0)
    {
        var test = handler;
        if (test == null)
        {
            return;
        }
        test.Invoke(val0);
    }

    public static void Raise(this EventHandler handler, object sender, EventArgs e = null)
    {
        var test = handler;
        if (test == null)
        {
            return;
        }
        test.Invoke(sender, e);
    }

    public static void Raise<T>(this EventHandler<T> handler, object sender, T e) where T : EventArgs
    {
        var test = handler;
        if (test == null)
        {
            return;
        }
        test.Invoke(sender, e);
    }

    public static void Raise<T>(this EventHandler<EventArgs<T>> handler, object sender, T value)
    {
        var test = handler;
        if (test == null) return;
        test.Invoke(sender, new EventArgs<T>(value));
    }
}

public static class SystemExtensions
{
    public static string RemoveNumbers(this string str)
    {
        return str.Replace("", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9");
    }

    public static string Replace(this string str, string replacement, params string[] toReplace)
    {
        str = toReplace.Aggregate(str, (current, c) => current.Replace(c, replacement));
        return str;
    }
}
