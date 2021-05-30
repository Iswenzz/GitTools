using System;

namespace Iswenzz.GitTools.Utils
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GitRemoteAttribute : ReflectiveAttribute
    {
        public string Name { get; set; }

        public GitRemoteAttribute(string name)
        {
            Name = name;
            ReflectMetadata = $"{nameof(GitRemoteAttribute)}-{Name}";
        }
    }
}
