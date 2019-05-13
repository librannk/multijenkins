using StorageSpace.API.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorageSpace.API.BusinessLayer.Contracts
{
    /// <summary>
    ///  IISAManager
    /// </summary>
    public interface IISAManager
    {
        #region Methods
        /// <summary>
        /// Returns list of ISA by facilityId
        /// </summary>
        /// <param name="facilityId">facility Id</param>
        /// <returns></returns>
        Task<List<ISARequest>> GetISA(Guid facilityId);

        /// <summary>
        /// Returns ISA by isa Id and facility Id
        /// </summary>
        /// <param name="isaId">isa Id</param>
        /// <param name="facilityId">facility Id</param>
        /// <returns></returns>
        Task<ISA> GetISAById(string isaId, Guid facilityId);

        #endregion
    }
}
