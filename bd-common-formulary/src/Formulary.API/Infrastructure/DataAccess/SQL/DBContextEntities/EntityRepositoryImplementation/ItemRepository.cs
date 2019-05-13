using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using Formulary.API.Infrastructure.DataAccess.SQL.EFRepository;
using Formulary.API.Model.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryImplementation
{
    /// <summary>
    /// ItemRepository class implements the all member of IItemRepository of type Item.
    /// </summary> 
    public class ItemRepository : BaseRepository<ItemEntity>, IItemRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unitOfWork"></param>
        public ItemRepository(ApplicationDBContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all system items for list.
        /// </summary>
        /// <returns>Task&lt;List&lt;ItemEntity&gt;&gt;.</returns>
        public Task<List<ItemEntity>> GetAllMedicationItemsForList()
        {

            var result = DbSet
                .AsQueryable()
                .Include(b => b.MedicationItem)
                .Include(b => b.ProductIdentifications)
                .Include(c=>c.PreferredOrderings)
                .Where(b => b.MedicationItemFlag == true)
                .ToListAsync();
            return result;
        }

        /// <summary>
        /// Gets the item by identifier.
        /// </summary>
        /// <param name="ItemKey">The item key.</param>
        /// <returns>Task&lt;ItemEntity&gt;.</returns>
        public Task<ItemEntity> GetItemById(Guid ItemKey)
        {
            return DbSet
                .AsQueryable()
                .Include(b => b.MedicationItem)
                .Include(b => b.ProductIdentifications)
                .Include(b => b.PreferredOrderings)
                .FirstOrDefaultAsync(b => b.ItemKey == ItemKey);
        }

        /// <summary>
        /// Updates the system item set up.
        /// </summary>
        /// <param name="itemEntity">The item entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> UpdateSystemItemSetUp(ItemEntity itemEntity)
        {
            Update(itemEntity);
            itemEntity.LastModifiedDateTime = DateTimeOffset.Now;
            //TODO: Actor
            return await _unitOfWork.CommitChanges() > 0;
        }
        /// <summary>
        /// GetItemByCode
        /// </summary>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        public async Task<ItemEntity> GetItemByCode(string ItemId)
        {
            return await DbSet.FirstOrDefaultAsync(b => b.ItemID.Equals(ItemId));
        }
        /// <summary>
        /// add ItemEntity to database
        /// </summary>
        /// <param name="itemEntity"></param>
        /// <returns></returns>
        public async Task<bool> AddSystemItem(ItemEntity itemEntity)
        {
             await Add(itemEntity);
            //TODO: Actor
            return await _unitOfWork.CommitChanges() > 0;
        }

    }
}
