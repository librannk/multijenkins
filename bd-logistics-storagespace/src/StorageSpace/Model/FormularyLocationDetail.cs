using System.Collections.Generic;
using StorageSpace.API.Infrastructure.DataAccess.Mongo.Entities;

namespace StorageSpace.API.Model
{
    /// <summary> Formulary location details </summary>
    public class FormularyLocationDetail : Entity
    {
        #region Auto-Properties
        /// <summary> Formulary Identifier </summary>
        public int FormularyId { get; set; }

        /// <summary> Medicine identifier </summary>
        public int ItemId { get; set; }

        /// <summary> Storage spaces </summary>
        public List<Device> Devices { get; set; }
        #endregion
    }
}
