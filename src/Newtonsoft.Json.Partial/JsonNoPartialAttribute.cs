namespace Newtonsoft.Json.Partial
{
    using System;

    /// <summary>
    /// Defines that the given property should not be used
    /// for partial DTOs.
    /// If validation detects that such a property has been
    /// set the validation will fail.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class JsonNoPartialAttribute : Attribute
    {
    }
}
