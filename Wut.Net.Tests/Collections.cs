namespace Wut.Net.Tests;

public class Collections
{
    /// <summary>
    /// Verifies the precedence of null-coalescence over range spreading
    /// when cloning a collection.
    /// </summary>
    [Fact]
    public void WhenRangeSpreadingACollectionNullCoalescenceTakePrecedence()
    {
        IReadOnlyCollection<string>? value = null;
        // ReSharper disable once UseCollectionExpression <-- Testing explicity Array.Empty usage
        IReadOnlyCollection<string> cloned = [.. value ?? Array.Empty<string>()];
        Assert.Empty(cloned);
    }
}
