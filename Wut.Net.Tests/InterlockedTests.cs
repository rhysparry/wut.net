namespace Wut.Net.Tests;

public class InterlockedTests
{
    /// <summary>
    /// Demonstrates a technique to run only once by using <see cref="Interlocked.Exchange"/>
    /// </summary>
    [Fact]
    public void InterlockedCanBeUsedToEnsureAMethodIsOnlyExecutedOnce()
    {
        var fixture = new Fixture();
        var addValueToMeIfYouDo = new List<int>();

        Parallel.For(0, 100, _ => fixture.ExecuteOnlyOnce(addValueToMeIfYouDo));

        // If the method was executed more than once, this will be greater than 1
        Assert.Single(addValueToMeIfYouDo);
    }

    class Fixture
    {
        private bool _hasBeenExecuted;

        public void ExecuteOnlyOnce(List<int> addValueToMeIfYouDo)
        {
            // This guard will return early if we've already executed
            if (Interlocked.Exchange(ref _hasBeenExecuted, true))
            {
                return;
            }

            lock (addValueToMeIfYouDo)
            {
                addValueToMeIfYouDo.Add(1);
            }
        }
    }
}
