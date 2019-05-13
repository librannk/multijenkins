using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Infrastructure.DataAccess.SQL.EFRepository;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts
{
    /// <summary>
    /// IItemRepository interface represent the all member of IRepository of type Item.
    /// IItemRepository provide the extensibility for new operation other than IRepository.
    /// </summary> 
    public interface IItemRepository : IRepository<ItemEntity>
    {
        /// <summary>
        /// Gets all medication items for list.
        /// </summary>
        /// <returns>Task&lt;List&lt;ItemEntity&gt;&gt;.</returns>
        Task<List<ItemEntity>> GetAllMedicationItemsForList();

        /// <summary>
        /// Gets the item by identifier.
        /// </summary>
        /// <param name="ItemKey">The item key.</param>
        /// <returns>Task&lt;ItemEntity&gt;.</returns>
        Task<ItemEntity> GetItemById(Guid ItemKey);

        /// <summary>
        /// Updates the system item set up.
        /// </summary>
        /// <param name="itemEntity">The item entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> UpdateSystemItemSetUp(ItemEntity itemEntity);
        /// <summary>
        /// Get Exist ItemID
        /// </summary>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        Task<ItemEntity> GetItemByCode(string ItemId);
        /// <summary>
        /// AddSystemItem
        /// </summary>
        /// <param name="itemEntity"></param>
        /// <returns></returns>
        Task<bool> AddSystemItem(ItemEntity itemEntity);
    }
}
