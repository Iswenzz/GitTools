using System;
using System.Reflection;

namespace Iswenzz.GitTools.Utils
{
    public static class Reflect
    {
        public static Assembly CurrentAssembly { get; set; }

        static Reflect()
        {
            CurrentAssembly = Assembly.GetExecutingAssembly();
        }

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
