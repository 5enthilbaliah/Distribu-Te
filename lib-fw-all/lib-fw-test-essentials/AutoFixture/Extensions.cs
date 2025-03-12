namespace DistribuTe.Framework.TestEssentials.AutoFixture;

using global::AutoFixture;

public static class Extensions
{
    public static string GenerateLengthLimitedString(this Fixture instance, int length = 100)
    {
        return string.Join("", instance.CreateMany<char>(length).ToArray());
    }
}