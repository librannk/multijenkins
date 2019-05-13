using StorageSpace.API.Model;
using StorageSpace.API.Infrastructure.DataAccess.Mongo;
using StorageSpace.API.Infrastructure.DataAccess.Mongo.Clients;
using StorageSpace.API.Infrastructure.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StorageSpace.API.Common;
using BD.Core.ElasticClient.Mongo;

//TODO : to be refactored
namespace StorageSpace.API.Infrastructure.Repository.Concrete
{
    /// <summary>
    /// ISADetailRepository
    /// </summary>
    internal class ISADetailRepository : BaseRepository<ItemStorageSpace>, IISADetailRepository
    {
        public ISADetailRepository(MongoDbClient mongoClient)
            : base(mongoClient)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="facilityId"></param>
        /// <returns></returns>
        public async Task<List<ISARequest>> GetISA(Guid facilityId)
        {
            var collections = await (this as IISADetailRepository).GetAllAsync();
            List<ItemStorageSpace> isaType = collections.Where(s => s.DocType == Constant.ISAType && s.FacilityKey == facilityId).ToList();
            List<ItemStorageSpace> carousalType = collections.Where(s => s.DocType == Constant.CarousalType).ToList();
            List<ISARequest> isaRequestList = new List<ISARequest>();

            if (isaType.Count() > 0)
            {
                foreach (var item in isaType)
                {
                    ISARequest isaRequest = new ISARequest();
                    isaRequest.IsaId = Convert.ToString(item.Id);
                    isaRequest.Description = item.Description;
                    isaRequest.Active = item.ActiveFlag;
                    isaRequest.IsaType = carousalType.Where(s => Convert.ToString(item.Carousel) == s.Id).Select(s => s.Description).FirstOrDefault();
                    isaRequestList.Add(isaRequest);
                }
            }
            
            return isaRequestList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isaId"></param>
        /// <param name="facilityId"></param>
        /// <returns></returns>
        public async Task<ItemStorageSpace> GetISAById(string isaId, Guid facilityId)
        {
            var isa = await (this as IISADetailRepository).GetAsync(isaId);
            if (isa != null && isa.FacilityKey == facilityId && isa.DocType == Constant.ISAType)
            {
                return isa;
            }
            return null;
        }
    }
}
