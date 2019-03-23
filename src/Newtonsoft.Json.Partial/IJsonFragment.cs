namespace Newtonsoft.Json.Partial
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Info about an used JSON fragment.
    /// </summary>
    public interface IJsonFragment
    {
        /// <summary>
        /// Gets the deserialized object.
        /// </summary>
        Object Data { get; }

        /// <summary>
        /// Gets the used keys.
        /// </summary>
        IEnumerable<String> Keys { get; }
    }
}
