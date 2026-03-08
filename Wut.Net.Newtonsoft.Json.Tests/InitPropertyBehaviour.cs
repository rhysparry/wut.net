using Newtonsoft.Json;
using Xunit;

namespace Wut.Net.Newtonsoft.Json.Tests;

public class InitPropertyBehaviour
{
    /// <summary>
    /// Verifies that when deserializing JSON that includes a value for an
    /// init-only property, the JSON value is used rather than the default.
    /// </summary>
    [Fact]
    public void WhenDeserializingJsonWithInitPropertyValueThenJsonValueIsUsed()
    {
        var json = """{"Value":"from-json"}""";
        var instance = JsonConvert.DeserializeObject<ClassWithDefault>(json);
        Assert.NotNull(instance);
        Assert.Equal("from-json", instance.Value);
    }

    /// <summary>
    /// Verifies that when deserializing JSON that does not include a value for
    /// an init-only property with a default, the default value is preserved.
    /// </summary>
    [Fact]
    public void WhenDeserializingJsonWithoutInitPropertyValueThenDefaultIsPreserved()
    {
        var json = "{}";
        var instance = JsonConvert.DeserializeObject<ClassWithDefault>(json);
        Assert.NotNull(instance);
        Assert.Equal("default", instance.Value);
    }

    /// <summary>
    /// Verifies that, similar to regular object initialization, the default
    /// initialization code for an init-only property is still executed during
    /// JSON deserialization even when a value is provided in the JSON.
    /// The JSON value takes precedence over the default.
    /// </summary>
    [Fact]
    public void WhenDeserializingJsonWithInitPropertyValueDefaultCodeIsStillRun()
    {
        var before = ClassWithTrackedDefault.InitializerCallCount;
        var json = """{"Value":"from-json"}""";
        var instance = JsonConvert.DeserializeObject<ClassWithTrackedDefault>(json);
        Assert.NotNull(instance);
        Assert.Equal("from-json", instance.Value);
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
