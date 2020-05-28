using System;

namespace Framework
{
    public static class TypeExtensions
    {
        public static string GetQueueName(this Type type) =>
            $"{type.FullName?.ToLowerInvariant().Replace('.', '-') ?? "not-defined"}";
        
        public static Uri GetUriForMessage(this Type type) =>
            new Uri($"queue:{type.GetQueueName()}");
    }
}