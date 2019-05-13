using System;

namespace TransactionQueue.API.Common.Helpers
{
    /// <summary>
    /// Helper methods for string
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// compare two string values
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool IsEqual(string value1, string value2)
        {
            return string.Equals(value1, value2, StringComparison.OrdinalIgnoreCase);
        }
    }
}
