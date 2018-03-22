using System;

namespace Brightgrove.FruitBasket.Utils
{
    public static class Check
    {
        public static void NotNull<T>(T value, string name)
        {
            if (value == null)
                throw new ArgumentNullException(name);
        }

        public static void NotNullOrEmpty(string value, string name)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(name);
        }
    }
}
