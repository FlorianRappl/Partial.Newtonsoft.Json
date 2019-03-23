namespace Newtonsoft.Json.Partial
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Wrapper for an arbitrary JsonReader instance that keeps
    /// track of the (top-level) properties.
    /// Should only be applied on objects (i.e., assumes that
    /// we've already seen a StartObject token).
    /// </summary>
    public class WrappedJsonReader : JsonReader
    {
        private readonly JsonReader _reader;
        private Int32 _level = 0;

        /// <summary>
        /// Wraps the given JsonReader.
        /// </summary>
        /// <param name="reader">The reader instance to wrap.</param>
        public WrappedJsonReader(JsonReader reader)
        {
            _reader = reader;
        }

        /// <summary>
        /// Gets the used keys.
        /// </summary>
        public List<String> Keys { get; } = new List<String>();

        /// <inheritdoc />
        public override Boolean Read()
        {
            var result = _reader.Read();

            if (_reader.TokenType == JsonToken.StartObject)
            {
                _level++;
            }
            else if (_reader.TokenType == JsonToken.EndObject)
            {
                _level--;
            }
            else if (_level == 0 && _reader.TokenType == JsonToken.PropertyName)
            {
                Keys.Add(Value as string);
            }

            return result;
        }

        /// <inheritdoc />
        public override Char QuoteChar => _reader.QuoteChar;

        /// <inheritdoc />
        public override JsonToken TokenType => _reader.TokenType;

        /// <inheritdoc />
        public override Object Value => _reader.Value;

        /// <inheritdoc />
        public override Type ValueType => _reader.ValueType;

        /// <inheritdoc />
        public override Int32 Depth => _reader.Depth;

        /// <inheritdoc />
        public override String Path => _reader.Path;

        /// <inheritdoc />
        public override Int32? ReadAsInt32() => _reader.ReadAsInt32();

        /// <inheritdoc />
        public override String ReadAsString() => _reader.ReadAsString();

        /// <inheritdoc />
        public override Byte[] ReadAsBytes() => _reader.ReadAsBytes();

        /// <inheritdoc />
        public override Double? ReadAsDouble() => _reader.ReadAsDouble();

        /// <inheritdoc />
        public override Boolean? ReadAsBoolean() => _reader.ReadAsBoolean();

        /// <inheritdoc />
        public override Decimal? ReadAsDecimal() => _reader.ReadAsDecimal();

        /// <inheritdoc />
        public override DateTime? ReadAsDateTime() => _reader.ReadAsDateTime();

        /// <inheritdoc />
        public override DateTimeOffset? ReadAsDateTimeOffset() => _reader.ReadAsDateTimeOffset();

        /// <inheritdoc />
        public override void Close() => _reader.Close();
    }
}
