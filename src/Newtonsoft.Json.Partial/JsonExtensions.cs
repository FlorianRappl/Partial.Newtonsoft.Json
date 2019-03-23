namespace Newtonsoft.Json.Partial
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Extensions for the JSON DTOs using reflection.
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// Gets the JSON property name of the provided .NET DTO property.
        /// Either uses the property name specified in the JsonPropertyAttribute
        /// or the standard property name.
        /// </summary>
        /// <param name="info">The reflection info about the property.</param>
        /// <returns>The name of the property.</returns>
        public static String GetJsonPropertyName(this PropertyInfo info)
        {
            var name = info.Name;
            var attr = info.GetCustomAttribute<JsonPropertyAttribute>();
            return attr?.PropertyName ?? name;
        }

        /// <summary>
        /// Gets the property info of the provided .NET type by using the JSON declared
        /// property name.
        /// </summary>
        /// <param name="type">The reflection info about the type.</param>
        /// <param name="jsonPropertyName">The given JSON property name.</param>
        /// <returns>The reflection property info.</returns>
        public static PropertyInfo GetPropertyFromJson(this Type type, String jsonPropertyName)
        {
            foreach (var property in type.GetProperties())
            {
                if (property.GetJsonPropertyName().Equals(jsonPropertyName, StringComparison.InvariantCulture))
                {
                    return property;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets if the property should be ignored in JSON. This is true if
        /// a JsonIgnoreAttribute has been found.
        /// </summary>
        /// <param name="info">The reflection info about the property.</param>
        /// <returns>True if it should be ignored in JSON, otherwise false.</returns>
        public static Boolean IsJsonIgnored(this PropertyInfo info)
        {
            var name = info.Name;
            var attr = info.GetCustomAttribute<JsonIgnoreAttribute>();
            return attr != null;
        }

        /// <summary>
        /// Gets if the property should be forbidden in (partial) JSON. This is true if
        /// a IgnoreInPartialPutAttribute has been found.
        /// </summary>
        /// <param name="info">The reflection info about the property.</param>
        /// <returns>True if it should be ignored in JSON, otherwise false.</returns>
        public static Boolean IsJsonForbidden(this PropertyInfo info)
        {
            var name = info.Name;
            var attr = info.GetCustomAttribute<JsonNoPartialAttribute>();
            return attr != null;
        }
    }
}
