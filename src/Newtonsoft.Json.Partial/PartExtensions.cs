namespace Newtonsoft.Json.Partial
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Extensions for the Part class.
    /// </summary>
    public static class PartExtensions
    {
        /// <summary>
        /// Validates the provided partial DTO by walking over all provided keys and
        /// checking if they are ignored or even forbidden for a partial DTO.
        /// The validation also fails if keys are given that are not specified for
        /// the DTO.
        /// </summary>
        /// <typeparam name="T">The type of partial DTO.</typeparam>
        /// <param name="partialInput">The partial input to use as basis.</param>
        /// <returns>True if the partial input is valid, otherwise false.</returns>
        public static Boolean Validate<T>(this Part<T> partialInput)
        {
            foreach (var key in partialInput.Keys)
            {
                var type = typeof(T);
                var info = type.GetPropertyFromJson(key);

                if (info == null || !partialInput.IsSet(info))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines if the selected property has been set. If it was specified
        /// the callback (if provided) will be invoked with the specified value.
        /// </summary>
        /// <typeparam name="T">The type of partial DTO.</typeparam>
        /// <typeparam name="TProperty">The type of the selected property.</typeparam>
        /// <param name="partialInput">The partial input to use as basis.</param>
        /// <param name="property">The property selection.</param>
        /// <param name="onAvailable">The optional callback if it has been set.</param>
        /// <returns>True if the property has been specified, otherwise false.</returns>
        public static Boolean IsSet<T, TProperty>(this Part<T> partialInput, Expression<Func<T, TProperty>> property, Action<TProperty> onAvailable = null)
        {
            var info = partialInput.Data.GetPropertyInfo(property);
            var available = partialInput.IsSet(info);

            if (available)
            {
                onAvailable?.Invoke((TProperty)info.GetValue(partialInput.Data));
            }

            return available;
        }

        /// <summary>
        /// Iterates over all provided keys yielding the values.
        /// </summary>
        /// <typeparam name="T">The type of the partial DTO.</typeparam>
        /// <param name="partialInput">The partial input to use as basis.</param>
        /// <returns>The enumerable over all key value pairs.</returns>
        public static IEnumerable<KeyValuePair<String, Object>> GetValues<T>(this Part<T> partialInput)
        {
            foreach (var key in partialInput.Keys)
            {
                var type = typeof(T);
                var info = type.GetPropertyFromJson(key);

                if (info != null && partialInput.IsSet(info))
                {
                    yield return new KeyValuePair<string, object>(info.Name, info.GetValue(partialInput.Data));
                }
            }
        }

        /// <summary>
        /// Determines if the given property info is set on the partial input.
        /// </summary>
        /// <typeparam name="T">The type of the partial input.</typeparam>
        /// <param name="partialInput">The partial input holder.</param>
        /// <param name="info">The property info to get the info for.</param>
        /// <returns>True if the property has been set, otherwise false.</returns>
        private static Boolean IsSet<T>(this Part<T> partialInput, PropertyInfo info)
        {
            if (!info.IsJsonIgnored() && !info.IsJsonForbidden())
            {
                var key = info.GetJsonPropertyName();
                return partialInput.Keys.Contains(key);
            }

            return false;
        }

        /// <summary>
        /// Gets the reflection property infos. The property is selected via
        /// C# selector.
        /// </summary>
        /// <typeparam name="T">The type of the underyling class.</typeparam>
        /// <typeparam name="TProperty">The type of the selected property.</typeparam>
        /// <param name="source">The source instance.</param>
        /// <param name="propertyLambda">The property selector.</param>
        /// <returns>The reflection details on the selected property.</returns>
        private static PropertyInfo GetPropertyInfo<T, TProperty>(this T source, Expression<Func<T, TProperty>> propertyLambda)
        {
            var type = typeof(T);

            var member = propertyLambda.Body as MemberExpression ??
                throw new ArgumentException($"Expression '{propertyLambda.ToString()}' refers to a method, not a property.");

            var propInfo = member.Member as PropertyInfo ??
                throw new ArgumentException($"Expression '{propertyLambda.ToString()}' refers to a field, not a property.");

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException($"Expression '{propertyLambda.ToString()}' refers to a property that is not from type {type}.");

            return propInfo;
        }

    }
}
