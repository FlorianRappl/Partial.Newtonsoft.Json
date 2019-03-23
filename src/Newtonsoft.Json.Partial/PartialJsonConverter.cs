namespace Newtonsoft.Json.Partial
{
    using System;

    /// <summary>
    /// Converter responsible for deserializing a Part of T model.
    /// The deserialization is using the standard deserializer for the
    /// inner model. The final object (Part&lt;T&gt; instance) should
    /// be validated using the .Validate() extension method.
    /// </summary>
    public class PartialJsonConverter : JsonConverter
    {
        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer serializer)
        {
            var part = value as IJsonFragment;

            if (part != null)
            {
                var wrapper = new WrappedJsonWriter(writer, part.Keys);
                serializer.Serialize(wrapper, part.Data);
            }
        }

        /// <inheritdoc />
        public override Object ReadJson(JsonReader reader, Type objectType, Object existingValue, JsonSerializer serializer)
        {
            var innerType = objectType.GetGenericArguments()[0];
            var wrapper = new WrappedJsonReader(reader);
            var obj = serializer.Deserialize(wrapper, innerType);
            return Activator.CreateInstance(objectType, new[] { obj, wrapper.Keys });
        }

        /// <inheritdoc />
        public override Boolean CanWrite => true;

        /// <inheritdoc />
        public override Boolean CanRead => true;

        /// <inheritdoc />
        public override Boolean CanConvert(Type objectType) => objectType == typeof(Part<>);
    }
}
