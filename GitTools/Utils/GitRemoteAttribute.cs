using System;

namespace Iswenzz.GitTools.Utils
{
    /// <summary>
    /// Attribute to specify a git remote.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class GitRemoteAttribute : ReflectiveAttribute
    {
        public string Name { get; set; }

        /// <summary>
        /// Create a new remote.
        /// </summary>
        /// <param name="name">The remote name.</param>
        public GitRemoteAttribute(string name)
        {
            Name = name;
            ReflectMetadata = $"{nameof(GitRemoteAttribute)}-{Name}";
        }
    }
}
