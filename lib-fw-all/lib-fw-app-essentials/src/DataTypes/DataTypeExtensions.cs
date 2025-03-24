namespace DistribuTe.Framework.AppEssentials.DataTypes;

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
    
    public static long AsLong(this string instance)
    {
        var success = long.TryParse(instance, out long value);
        return success ? value : 0;
    }

    public static IEnumerable<long> AsLongEnumerable(this string instance, char delimiter = ',')
    {
        var delimited = string.Empty;
        var hashSet = new HashSet<long>();
        if (instance.StartsWith('(') && instance.EndsWith(')'))
            delimited = instance[1..^1];

        foreach (var uno in delimited.Split([delimiter]))
            if (!string.IsNullOrEmpty(uno))
                hashSet.Add(uno.AsLong());
        return hashSet;
    }
    
    public static decimal AsDecimal(this string instance)
    {
        var success = decimal.TryParse(instance, out decimal value);
        return success ? value : 0;
    }

    public static IEnumerable<decimal> AsDecimalEnumerable(this string instance, char delimiter = ',')
    {
        var delimited = string.Empty;
        var hashSet = new HashSet<decimal>();
        if (instance.StartsWith('(') && instance.EndsWith(')'))
            delimited = instance[1..^1];

        foreach (var uno in delimited.Split([delimiter]))
            if (!string.IsNullOrEmpty(uno))
                hashSet.Add(uno.AsDecimal());
        return hashSet;
    }
    
    public static DateTime AsDateTime(this string instance)
    {
        var success = DateTime.TryParse(instance, out DateTime value);
        return success ? value : new DateTime(2021, 01, 01);
    }

    public static IEnumerable<DateTime> AsDateTimeEnumerable(this string instance, char delimiter = ',')
    {
        var delimited = string.Empty;
        var hashSet = new HashSet<DateTime>();
        if (instance.StartsWith('(') && instance.EndsWith(')'))
            delimited = instance[1..^1];

        foreach (var uno in delimited.Split([delimiter]))
            if (!string.IsNullOrEmpty(uno))
                hashSet.Add(uno.AsDateTime());
        return hashSet;
    }
}