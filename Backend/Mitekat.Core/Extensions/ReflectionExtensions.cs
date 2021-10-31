namespace Mitekat.Core.Extensions
{
    using System;

    internal static class ReflectionExtensions
    {
        public static bool DerivesFrom(this Type type, Type baseTypeCandidate)
        {
            var currentType = type ?? throw new ArgumentException(null, nameof(type));

            while (currentType != typeof(object))
            {
                if (currentType!.IsGenericType && currentType.GetGenericTypeDefinition() == baseTypeCandidate)
                {
                    return true;
                }

                if (currentType == baseTypeCandidate)
                {
                    return true;
                }

                currentType = currentType!.BaseType;
            }

            return false;
        }
    }
}
