using System;
using System.Reflection;

namespace Iswenzz.GitTools.Utils
{
    /// <summary>
    /// Utility class to reflect attributes that use the ReflectAttribute class.
    /// </summary>
    public static class Reflect
    {
        public static Assembly CurrentAssembly { get; set; }

        /// <summary>
        /// Get the current Assembly.
        /// </summary>
        static Reflect()
        {
            CurrentAssembly = Assembly.GetExecutingAssembly();
        }

        /// <summary>
        /// Retrieve the type from a custom attribute.
        /// </summary>
        /// <param name="expectedValue">The metadata to search.</param>
        /// <returns></returns>
        public static Type RetrieveTypeFromCustomAttributeMetadata(string expectedValue)
        {
            foreach (Type type in CurrentAssembly.GetTypes())
            {
                ReflectiveAttribute attributes = type.GetCustomAttribute<ReflectiveAttribute>();
                if (attributes != null && attributes.ReflectMetadata == expectedValue)
                    return type;
            }
            return null;
        }
    }
}
