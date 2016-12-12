using System;
using System.Collections.Generic;
using System.Linq;

namespace decomplex.Extensions
{
    /// <summary>
    /// Extension methods for attributes.
    /// </summary>
    public static class AttributeExtensions
    {
        /// <summary>
        /// Returns attribute instance on an enum value or on a class instance.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute.</typeparam>
        /// <param name="classOrEnumInstance">A class instance or an enum instance.</param>
        /// <returns>Attribute instance if exists, otherwise null.</returns>
        public static TAttribute FindAttribute<TAttribute>(this object classOrEnumInstance) where TAttribute : Attribute
        {
            if (classOrEnumInstance == null) return null;

            return classOrEnumInstance.GetType().IsEnum 
                ? FindEnumAttributeValue<TAttribute>(classOrEnumInstance) 
                : FindClassAttribute<TAttribute>(classOrEnumInstance);
        }

        /// <summary>
        /// Gets all attributes of given type on a class instance.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute.</typeparam>
        /// <param name="classOrEnumInstance">A class instance or an enum instance.</param>
        /// <returns>Collection of all attributes of given type.</returns>
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this object classOrEnumInstance)
            where TAttribute : Attribute
        {
            if (classOrEnumInstance == null) return null;

            return classOrEnumInstance.GetType().IsEnum 
                ? GetEnumAttributeValues<TAttribute>(classOrEnumInstance)
                : GetClassAttributes<TAttribute>(classOrEnumInstance);
        }

        private static TAttribute FindClassAttribute<TAttribute>(object instanceWithAttribute) where TAttribute : Attribute
        {
            return (TAttribute)instanceWithAttribute.GetType().GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault();
        }

        private static TAttribute FindEnumAttributeValue<TAttribute>(object enumValue) where TAttribute : Attribute
        {
            var attributeField = enumValue.GetType().GetField(enumValue.ToString());

            return attributeField.GetCustomAttributes(typeof(TAttribute), false).OfType<TAttribute>().FirstOrDefault();
        }

        private static IEnumerable<TAttribute> GetClassAttributes<TAttribute>(object instanceWithAttribute)
                   where TAttribute : Attribute
        {
            return instanceWithAttribute.GetType().GetCustomAttributes(typeof(TAttribute), false).OfType<TAttribute>().ToList();
        }

        private static IEnumerable<TAttribute> GetEnumAttributeValues<TAttribute>(object classWithAttribute)
            where TAttribute : Attribute
        {
            var attributeField = classWithAttribute.GetType().GetField(classWithAttribute.ToString());

            return attributeField.GetCustomAttributes(typeof(TAttribute), false).OfType<TAttribute>().ToList();
        }
    }
}