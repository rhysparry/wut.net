namespace Wut.Net.Tests;

public class DefaultInterfaceImplementations
{
    /// <summary>
    /// Verifies that when a concrete class provides a property that shares the same
    /// name as a default interface implementation, accessing it through the concrete
    /// type returns the concrete class's value.
    /// </summary>
    [Fact]
    public void WhenAccessedAsConcreteTypeThenConcreteValueIsReturned()
    {
        var instance = new ConcreteClass();
        Assert.Equal("concrete", instance.Value);
    }

    /// <summary>
    /// Verifies that when a concrete class provides a property with the same name as
    /// a default interface implementation, accessing it through the interface that the
    /// abstract base directly implements returns the default interface implementation
    /// value rather than the concrete class's value.
    ///
    /// <see cref="AbstractBase"/> implements <see cref="ILayer"/>, but provides no
    /// class-level declaration of <see cref="IHaveADefaultValue.Value"/>. Because no
    /// class in the hierarchy establishes a mapping from a class member to the
    /// interface member, the default implementation defined on
    /// <see cref="IHaveADefaultValue"/> is used even though <see cref="ConcreteClass"/>
    /// has its own <c>Value</c> property.
    /// </summary>
    [Fact]
    public void WhenAccessedAsDirectlyImplementedInterfaceThenInterfaceDefaultIsReturned()
    {
        ILayer instance = new ConcreteClass();
        Assert.Equal("interface-default", instance.Value);
    }

    /// <summary>
    /// Verifies that when a concrete class provides a property with the same name as
    /// a default interface implementation, accessing it through the base interface
    /// where the default is defined also returns the default interface implementation
    /// value rather than the concrete class's value.
    ///
    /// Because <see cref="ILayer"/> does not redeclare <see cref="IHaveADefaultValue.Value"/>,
    /// and no class in the hierarchy establishes a mapping from a class member to that
    /// interface member, the same default value is returned whether the instance is
    /// accessed through <see cref="ILayer"/> or through <see cref="IHaveADefaultValue"/>
    /// directly.
    /// </summary>
    [Fact]
    public void WhenAccessedAsBaseInterfaceThenInterfaceDefaultIsReturned()
    {
        IHaveADefaultValue instance = new ConcreteClass();
        Assert.Equal("interface-default", instance.Value);
    }

    public interface IHaveADefaultValue
    {
        string Value => "interface-default";
    }

    public interface ILayer : IHaveADefaultValue
    {
        // Does not redeclare Value; the default implementation from
        // IHaveADefaultValue applies when accessed through either interface.
    }

    public abstract class AbstractBase : ILayer
    {
        // Does not declare Value; the default interface implementation applies
        // when this instance is accessed through ILayer or IHaveADefaultValue.
    }

    public class ConcreteClass : AbstractBase
    {
        // Provides its own Value, but does not list ILayer or IHaveADefaultValue
        // in its base type list. This means C# does not map this member to the
        // interface member, so the default interface implementation remains in
        // effect when the instance is accessed through either interface.
        public string Value => "concrete";
    }
}
