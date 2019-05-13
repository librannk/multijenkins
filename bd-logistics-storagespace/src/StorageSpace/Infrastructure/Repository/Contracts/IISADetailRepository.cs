using StorageSpace.API.Model;
using StorageSpace.API.Infrastructure.DataAccess.Mongo.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorageSpace.API.Infrastructure.Repository.Contracts
{
    /// <summary>
    /// IISADetailRepository
    /// </summary>
    public interface IISADetailRepository : IBaseRepository<ItemStorageSpace>
    {
        /// <summary>
        /// Get ISA List
        /// </summary>
        /// <param name="facilityId"></param>
        /// <returns></returns>
        Task<List<ISARequest>> GetISA(Guid facilityId);

        /// <summary>
        /// Get ISA Data
        /// </summary>
        /// <param name="isaId"></param>
        /// <param name="facilityId"></param>
        /// <returns></returns>
        Task<ItemStorageSpace> GetISAById(string isaId, Guid facilityId);

    }
}
