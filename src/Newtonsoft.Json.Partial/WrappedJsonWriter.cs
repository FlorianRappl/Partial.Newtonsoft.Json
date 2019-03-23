namespace Newtonsoft.Json.Partial
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Wrapper for an arbitrary JsonWriter instance that only emits
    /// the top-level properties that have been set using the provided
    /// keys.
    /// Should only be applied on Part objects.
    /// </summary>
    public class WrappedJsonWriter : JsonWriter
    {
        private readonly JsonWriter _writer;
        private readonly IEnumerable<String> _keys;
        private Int32 _level = 0;
        private Boolean _blocked = false;

        /// <summary>
        /// Wraps the given JsonWriter.
        /// </summary>
        /// <param name="writer">The writer instance to wrap.</param>
        /// <param name="keys">The keys that are available.</param>
        public WrappedJsonWriter(JsonWriter writer, IEnumerable<String> keys)
        {
            _writer = writer;
            _keys = keys;
        }

        /// <inheritdoc />
        public override void Close() => _writer.Close();

        /// <inheritdoc />
        public override void Flush() => _writer.Flush();

        /// <inheritdoc />
        public override void WriteWhitespace(String ws) => _writer.WriteWhitespace(ws);

        /// <inheritdoc />
        public override void WriteStartObject() => Increase(_writer.WriteStartObject);

        /// <inheritdoc />
        public override void WriteEndObject() => Decrease(_writer.WriteEndObject);

        /// <inheritdoc />
        public override void WriteStartConstructor(String name) => _writer.WriteStartConstructor(name);

        /// <inheritdoc />
        public override void WriteEndConstructor() => _writer.WriteEndConstructor();

        /// <inheritdoc />
        public override void WriteComment(String text) => _writer.WriteComment(text);

        /// <inheritdoc />
        public override void WriteEnd() => _writer.WriteEnd();

        /// <inheritdoc />
        public override void WriteStartArray() => _writer.WriteStartArray();

        /// <inheritdoc />
        public override void WriteEndArray() => _writer.WriteEndArray();

        /// <inheritdoc />
        public override void WriteNull() => _writer.WriteNull();

        /// <inheritdoc />
        public override void WritePropertyName(String name) => _writer.WritePropertyName(name);

        /// <inheritdoc />
        public override void WritePropertyName(String name, Boolean escape) => CheckName(() => _writer.WritePropertyName(name, escape), name);

        /// <inheritdoc />
        public override void WriteRaw(String json) => _writer.WriteRaw(json);

        /// <inheritdoc />
        public override void WriteRawValue(String json) => _writer.WriteRawValue(json);

        /// <inheritdoc />
        public override void WriteUndefined() => _writer.WriteUndefined();

        /// <inheritdoc />
        public override void WriteValue(String value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Object value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Uri value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Byte[] value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Boolean? value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Int32? value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(UInt32? value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Double? value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Int64? value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Single? value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(UInt64? value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Byte? value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Char? value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(DateTime? value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Decimal? value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(SByte? value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(TimeSpan? value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(DateTimeOffset? value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Guid? value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Int16? value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(UInt16? value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Boolean value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Int32 value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(UInt32 value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Double value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Int64 value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Single value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(UInt64 value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Byte value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Char value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(DateTime value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Decimal value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(SByte value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(TimeSpan value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(DateTimeOffset value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Guid value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(Int16 value) => CheckValue(_writer.WriteValue, value);

        /// <inheritdoc />
        public override void WriteValue(UInt16 value) => CheckValue(_writer.WriteValue, value);

        private void Increase(Action action)
        {
            _level++;
            action.Invoke();
        }

        private void Decrease(Action action)
        {
            _level--;
            action.Invoke();
        }

        private void CheckName(Action action, String name)
        {
            if (_level != 1 || _keys.Contains(name))
            {
                _blocked = false;
                action.Invoke();
            }
            else
            {
                _blocked = true;
            }
        }

        private void CheckValue<T>(Action<T> action, T value)
        {
            if (!_blocked)
            {
                action.Invoke(value);
            }
        }
    }
}
