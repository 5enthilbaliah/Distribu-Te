namespace DistribuTe.Framework.AppEssentials;

public static class DataTypeExtensions
{
    public static int AsInt(this string instance)
    {
        var success = int.TryParse(instance, out int value);
        return success ? value : 0;
    }

    public static IEnumerable<int> AsIntEnumerable(this string instance, char delimiter = ',')
    {
        var delimited = string.Empty;
        var hashSet = new HashSet<int>();
        if (instance.StartsWith('(') && instance.EndsWith(')'))
            delimited = instance[1..^1];

        foreach (var uno in delimited.Split([delimiter]))
            if (!string.IsNullOrEmpty(uno))
                hashSet.Add(uno.AsInt());
        return hashSet;
    }
}