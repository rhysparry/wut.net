using System.Threading;

namespace Wut.Net.Tests;

public class InitProperties
{
    /// <summary>
    /// Verifies that when an init-only property has a default value and no value
    /// is assigned during object initialization, the default value is used.
    /// </summary>
    [Fact]
    public void WhenInitPropertyHasDefaultAndNoValueAssignedThenDefaultIsUsed()
    {
        var instance = new ClassWithDefault();
        Assert.Equal("default", instance.Value);
    }

    /// <summary>
    /// Verifies that when an init-only property has a default value initializer,
    /// the default initialization code is still executed even when a value is
    /// assigned during object initialization. The assigned value takes precedence,
    /// but the default initializer code runs before being overwritten.
    /// </summary>
    [Fact]
    public void WhenInitPropertyHasDefaultAndValueIsAssignedDefaultCodeIsStillRun()
    {
        var before = ClassWithTrackedDefault.InitializerCallCount;
        var instance = new ClassWithTrackedDefault { Value = "assigned" };
        Assert.Equal("assigned", instance.Value);
        Assert.Equal(before + 1, ClassWithTrackedDefault.InitializerCallCount);
    }

    public class ClassWithDefault
    {
        public string Value { get; init; } = "default";
    }

    public class ClassWithTrackedDefault
    {
        public static int InitializerCallCount;

        public string Value { get; init; } = TrackAndReturnDefault();

        private static string TrackAndReturnDefault()
        {
            Interlocked.Increment(ref InitializerCallCount);
            return "default";
        }
    }
}
