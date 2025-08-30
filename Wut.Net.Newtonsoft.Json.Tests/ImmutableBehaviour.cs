using System.Collections.Immutable;
using Newtonsoft.Json;
using Xunit;

namespace Wut.Net.Newtonsoft.Json.Tests;

public class ImmutableBehaviour
{
    /// <summary>
    /// Verifies that when classes have init-only properties, Newtonsoft.Json
    /// can serialize and deserialize them correctly.
    /// </summary>
    [Fact]
    public void WhenPropertiesAreInitOnlyThenJsonDeserializationWorks()
    {
        var instance = new ImmutableClass { Id = 1, Name = "Test" };
        var json = JsonConvert.SerializeObject(instance);
        var deserializedInstance = JsonConvert.DeserializeObject<ImmutableClass>(json);
        Assert.NotNull(deserializedInstance);
        Assert.Equal(instance.Id, deserializedInstance.Id);
        Assert.Equal(instance.Name, deserializedInstance.Name);
    }

    /// <summary>
    /// Verifies that an immutable dictionary can be serialized and deserialized
    /// correctly using Newtonsoft.Json.
    /// </summary>
    [Fact]
    public void WhenUsingImmutableDictionarySerializationWorks()
    {
        var dict = new Dictionary<string, int> { { "One", 1 }, { "Two", 2 } };
        var immutableDict = dict.ToImmutableDictionary();
        var json = JsonConvert.SerializeObject(immutableDict);
        var deserializedDict = JsonConvert.DeserializeObject<ImmutableDictionary<string, int>>(
            json
        );
        Assert.NotNull(deserializedDict);
        Assert.Equal(immutableDict.Count, deserializedDict.Count);
        foreach (var kvp in immutableDict)
        {
            Assert.True(deserializedDict.ContainsKey(kvp.Key));
            Assert.Equal(kvp.Value, deserializedDict[kvp.Key]);
        }
    }

    /// <summary>
    /// Verifies that an immutable hash set can be serialized and deserialized
    /// correctly using Newtonsoft.Json.
    /// </summary>
    [Fact]
    public void WhenUsingImmutableHashSetSerializationWorks()
    {
        var set = new HashSet<int> { 1, 2, 3 };
        var immutableSet = set.ToImmutableHashSet();
        var json = JsonConvert.SerializeObject(immutableSet);
        var deserializedSet = JsonConvert.DeserializeObject<ImmutableHashSet<int>>(json);
        Assert.NotNull(deserializedSet);
        Assert.Equal(immutableSet.Count, deserializedSet.Count);
        foreach (var item in immutableSet)
        {
            Assert.Contains(item, deserializedSet);
        }
    }

    /// <summary>
    /// Newtonsoft.Json does not respect the `required` modifier during
    /// deserialization. This means you will not get deserialization errors if
    /// required properties are missing in the JSON. Consequently, you cannot rely
    /// on `required` properties to enforce presence during deserialization.
    ///
    /// System.Text.Json does respect `required` properties and will throw an
    /// exception if they are missing.
    /// </summary>
    [Fact]
    public void WhenDeserializingToClassWithRequiredPropertiesTheyAreNotRespected()
    {
        var value = JsonConvert.DeserializeObject<ClassWithRequiredProperties>("{}");
        Assert.NotNull(value);
        Assert.Equal(0, value.Id); // Default int value
        Assert.Null(value.Name); // Default string value
    }

    public class ImmutableClass
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }

    public class ClassWithRequiredProperties
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public required int Id { get; init; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public required string Name { get; init; }
    }
}
