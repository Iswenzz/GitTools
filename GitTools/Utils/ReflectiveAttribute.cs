using System;

namespace Iswenzz.GitTools.Utils
{
    /// <summary>
    /// Reflective attribute containing a ReflectMetadata property to indentify later.
    /// </summary>
    public class ReflectiveAttribute : Attribute
    {
        public string ReflectMetadata { get; set; }
    }
}
