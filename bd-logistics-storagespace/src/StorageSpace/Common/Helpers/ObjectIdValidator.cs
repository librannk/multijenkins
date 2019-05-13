using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace StorageSpace.API.Common.Helpers
{
    /// <summary>
    /// Helper class to Validate objectIds
    /// </summary>
    public static class ObjectIdValidator
    {
        /// <summary>
        /// Validates the specified object identifier.
        /// </summary>
        /// <param name="objId">The object identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool Validate(string objId)
        {
            return ObjectId.TryParse(objId, out var objectId);
        }
    }
}
