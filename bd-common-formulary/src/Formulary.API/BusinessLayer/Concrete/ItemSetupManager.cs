using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Formulary.API.BusinessLayer.Contract;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;
using Formulary.API.Model.ViewModel;
using Microsoft.Extensions.Logging;

namespace Formulary.API.BusinessLayer.Concrete
{
    /// <summary>
    /// ItemSetup Manager class
    /// </summary>
    public class ItemSetupManager : IItemSetupManager
    {
        #region Private variables
        //private readonly IFacilityRepository _facilityRepository;
        private readonly ILogger<ItemSetupManager> _logger;
        private readonly IItemRepository ItemRepository;
        private readonly IMapper mapper;
        #endregion

        public ItemSetupManager(IItemRepository ItemRepository, IMapper mapper)
        {
            this.ItemRepository = ItemRepository;
            this.mapper = mapper;
        }
        /// <summary>
        /// Function to get System ItemSetup List
        /// </summary>
        /// <returns>MedicationItemList</returns>
        /// 
        public async Task<List<MedicationItemList>> GetMedicationItems()
        {
            var objMedItems = await ItemRepository.GetAllMedicationItemsForList();

            return mapper.Map<List<ItemEntity>, List<MedicationItemList>>(objMedItems);
            //return await Task (objMedItems);
        }
    }
}
