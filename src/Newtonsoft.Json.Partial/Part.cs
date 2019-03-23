namespace Newtonsoft.Json.Partial
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Signals that the given DTO (type T) can be only partially specified
    /// in the incoming JSON fragment. The associated converter will fill both,
    /// the Data and the Keys, properties of the Part class.
    /// </summary>
    /// <typeparam name="T">The DTO to be filled out.</typeparam>
    [JsonConverter(typeof(PartialJsonConverter))]
    public class Part<T> : IJsonFragment
    {
        /// <summary>
        /// Creates a new partial instance using the given
        /// deserialized object and the set keys.
        /// </summary>
        /// <param name="data">The deserialized object.</param>
        /// <param name="keys">
        /// The used keys. These are the keys as set by the client.
        /// They do not necessarily match the property names.
        /// </param>
        public Part(T data, IEnumerable<String> keys)
        {
            Data = data;
            Keys = keys;
        }

        /// <inheritdoc />
        Object IJsonFragment.Data => Data;

        /// <summary>
        /// Gets the deserialized DTO.
        /// </summary>
        public T Data { get; }

        /// <summary>
        /// Gets the found keys.
        /// </summary>
        public IEnumerable<String> Keys { get; }
    }
}
